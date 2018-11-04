using RelatoriosCongregação.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RelatoriosCongregação.Controllers
{
    //todo: separa a interface para incluir/editar relatório.
    //todo: cria campo para identificar que relatório é de outro mês
    //todo: colocar totais
    //todo: criar relatório totalizando

    public class RelatoriosController : Controller
    {
        private FieldServiceEntities db = new FieldServiceEntities();

        public ActionResult DetalhadoIndividual(string anoMes, int? pagina)
        {
            var data = DateTime.Now.AddMonths(-1);
            if (string.IsNullOrEmpty(anoMes))
            {
                anoMes = data.Year.ToString() + data.Month.ToString().PadLeft(2, '0');
            }

            if (!pagina.HasValue || pagina.Value <= 0)
            {
                pagina = 1;
            }

            var relatorio = db.Relatorios
                                .Where(r => r.AnoMes == anoMes)
                                .OrderBy(r => r.Publicadores.Nome)
                                .Select(r => r)
                                .Skip(pagina.Value - 1)
                                .Take(1)
                                .FirstOrDefault();

            ViewBag.QtdTotal = db.Relatorios
                                .Where(r => r.AnoMes == anoMes)
                                .Count();

            LoadAnos(data.Year);
            LoadMeses(data.Month);

            ViewBag.anoMes = anoMes;

            if (relatorio == null)
            {
                relatorio = new Relatorios() { AnoMes = anoMes, IdTipo = 1 };
            }

            return View(relatorio);
        }

        public List<PublicadoresPorGrupo> GetIrregulares(string anoMes)
        {
            var publicadores = db.Publicadores
                            .Where(p => !p.Relatorios.Any(r => r.AnoMes == anoMes) && p.Ativo == true)
                            .GroupBy(p => p.Grupo.Dirigente)
                            .Select(p => new PublicadoresPorGrupo
                            {
                                Dirigente = p.Key,
                                Publicadores = p.Select(c => c.Nome).OrderBy(c => c).ToList()
                            })
                            .OrderBy(p => p.Dirigente)
                            .ToList();
            return publicadores;
        }

        public ActionResult Index(string ano, string mes, int? tipo)
        {
            string anoMes;

            if (string.IsNullOrEmpty(ano) || string.IsNullOrEmpty(mes))
            {
                var data = DateTime.Now.AddMonths(-1);
                anoMes = data.Year.ToString() + data.Month.ToString().PadLeft(2, '0');

                LoadAnos(data.Year);
                LoadMeses(data.Month);
            }
            else
            {
                anoMes = ano + mes.PadLeft(2, '0');
                LoadAnos(int.Parse(ano));
                LoadMeses(int.Parse(mes));
            }

            LoadPublicadores();
            LoadTipos();
            var geral = new RelatorioGeral();
            geral.Detalhado = GetRelatorioDetalhado(anoMes, tipo);
            geral.Totais = GetTotais(anoMes);
            geral.Irregulares = GetIrregulares(anoMes);

            return View(geral);
        }

        public IQueryable<Relatorios> GetRelatorioDetalhado(string anoMes, int? tipo)
        {
            var relatorios = db.Relatorios.Include("Publicadores")
                             .Include("Tipos")
                             .Where(r => r.AnoMes == anoMes);

            if (tipo.HasValue && tipo != 0)
            {
                relatorios = relatorios.Where(c => c.IdTipo == tipo);
            }

            return relatorios
                    .OrderByDescending(r => r.Horas)
                    .Select(r => r); 
        }

        public IQueryable<TotaisRelatorio> GetTotais(string anoMes)
        {
            return db.Relatorios.Include("Tipos")
                                .Where(r => r.AnoMes == anoMes)
                                .OrderBy(r => r.Publicadores.Nome)
                                .GroupBy(r => r.Tipos.Tipo)
                                .Select(r => new TotaisRelatorio
                                {
                                    Tipo = r.Key,
                                    QtdPublicadores = r.Count(),
                                    Horas = r.Sum(t => t.Horas),
                                    Publicacoes = r.Sum(t => t.Publicacoes),
                                    Videos = r.Sum(t => t.Videos),
                                    Revisitas = r.Sum(t => t.Revisitas),
                                    Estudos = r.Sum(t => t.Estudos)
                                });
        }
        private void LoadTipos()
        {
            var tipos = db.Tipos
                            .OrderBy(r => r.Tipo)
                            .ToList();

            tipos.Insert(0, new Tipos { Id = 0, Tipo = "" });
            ViewBag.Tipos = new SelectList(tipos, "Id", "Tipo");
        }

        public void LoadAnos(int selectedAno)
        {
            var anos = new List<int>();
            anos.Add(DateTime.Now.Year);
            anos.Add(DateTime.Now.AddYears(-1).Year);
            ViewBag.Anos = new SelectList(anos, selectedAno); ;
        }

        private void LoadMeses(int selectedMonth)
        {
            var numMeses = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var nomeMeses = numMeses.Select(n =>
                        new
                        {
                            numMes = n,
                            Nome = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(n)
                        });
            //var selectedItem = nomeMeses.Where(m => m.numMes == selectedMonth).FirstOrDefault();

            var listMeses = new SelectList(nomeMeses, "numMes", "Nome", selectedMonth);

            //var selected = listMeses.Where(x => x.Value == selectedMonth.ToString()).First();
            //listMeses.se= selectedItem.ToString();

            ViewBag.Meses = listMeses;
        }

        public ActionResult AnoMes()
        {
            var anos = new List<string>();
            anos.Add(DateTime.Now.Year.ToString());
            anos.Add(DateTime.Now.AddYears(-1).Year.ToString());

            return View(anos);
        }

        private void LoadPublicadores()
        {
            var publicadores = db.Publicadores
                                    .Where(p => p.Ativo == true)
                                    .OrderBy(r => r.Nome)
                                    .Select(r => new { r.Id, r.Nome })
                                    .ToList();

            publicadores.Insert(0, new { Id = 0, Nome = "" });
            ViewBag.Publicadores = new SelectList(publicadores, "Id", "Nome");
        }

        private void LoadPublicadores(string anoMes)
        {
            var publicadores = db.Publicadores
                                    .Where(p => p.Ativo == true && !p.Relatorios.Any(r => r.AnoMes == anoMes))
                                    .OrderBy(r => r.Nome)
                                    .Select(r => new { r.Id, r.Nome })
                                    .ToList();

            publicadores.Insert(0, new { Id = 0, Nome = "" });
            ViewBag.Publicadores = new SelectList(publicadores, "Id", "Nome");
        }

        // GET: Relatorios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        private void LoadCombosCadastro()
        {
            DateTime data = DateTime.Today.AddMonths(-1);
            LoadAnos(data.Year);
            LoadMeses(data.Month);
            LoadTipos();
            LoadPublicadores();
        }

        // GET: Relatorios/Create
        //public ActionResult Create()
        //{
        //    DateTime data = DateTime.Today.AddMonths(-1);
        //    LoadAnos(data.Year);
        //    LoadMeses(data.Month);
        //    LoadTipos();
        //    LoadPublicadores(data.Year.ToString() + data.Month.ToString().PadLeft(2, '0'));
        //    return View();
        //}

        public ActionResult Create(bool? showAll)
        {
            if (!showAll.HasValue || showAll.Value == true)
            {
                LoadCombosCadastro();
            }
            else
            {
                DateTime data = DateTime.Today.AddMonths(-1);
                LoadAnos(data.Year);
                LoadMeses(data.Month);
                LoadTipos();
                LoadPublicadores(data.Year.ToString() + data.Month.ToString().PadLeft(2, '0'));
            }
            return View();
        }

        // POST: Relatorios/Create
        [HttpPost]
        public ActionResult Create(Relatorios relatorio)
        {
            try
            {
                relatorio.AnoMes = relatorio.Ano.ToString() + relatorio.Mes.ToString().PadLeft(2, '0');
                db.Relatorios.Add(relatorio);

                var publicador = db.Publicadores.Where(p => p.Id == relatorio.IdPublicador).First();
                publicador.IdTipo = relatorio.IdTipo;

                db.SaveChanges();
                return RedirectToAction("Create", new { showAll = false });
            }
            catch
            {
                return View();
            }
        }

        // GET: Relatorios/Edit/5
        public ActionResult Edit(int id)
        {
            // LoadCombosCadastro();
            var relatorio = db.Relatorios.Find(id);
            relatorio.Ano = int.Parse(relatorio.AnoMes.Substring(0, 4));
            relatorio.Mes = int.Parse(relatorio.AnoMes.Substring(4, 2));
            LoadAnos(relatorio.Ano);
            LoadMeses(relatorio.Mes);
            LoadTipos();
            LoadPublicadores();
            return View(relatorio);
        }

        // POST: Relatorios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Relatorios relatorio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    relatorio.AnoMes = relatorio.Ano.ToString() + relatorio.Mes.ToString().PadLeft(2, '0');
                    db.Entry(relatorio).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", new
                    {
                        Pagina = Request.QueryString["Pagina"],
                        anoMes = Request.QueryString["anoMes"]
                    });
                }
                LoadCombosCadastro();
                return View(relatorio);
            }
            catch (Exception)
            {
                LoadCombosCadastro();
                return View(relatorio);
            }
        }

        // GET: Relatorios/Delete/5
        public ActionResult Delete(int id)
        {
            var relatorio = db.Relatorios.Find(id);
            relatorio.Ano = int.Parse(relatorio.AnoMes.Substring(0, 4));
            relatorio.Mes = int.Parse(relatorio.AnoMes.Substring(4, 2));
            return View(relatorio);
        }

        // POST: Relatorios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var relatorio = db.Relatorios.Find(id);
                db.Relatorios.Remove(relatorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }


}
