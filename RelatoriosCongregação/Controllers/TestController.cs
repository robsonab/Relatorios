using RelatoriosCongregação.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RelatoriosCongregação.Controllers
{
    public class TestController : Controller
    {
        private FieldServiceEntities db = new FieldServiceEntities();

        // GET: Test
        public ActionResult Index()
        {
            LoadGrupos();
            LoadPublicadores();
            LoadTipos();
            return View();
        }

        private void LoadPublicadores()
        {
            var publicadores = db.Publicadores
                            .OrderBy(r => r.Nome)
                            .ToList();
            ViewBag.Publicadores = new SelectList(publicadores, "Id", "Nome");
        }

        public void LoadGrupos()
        {
            var grupos = db.Grupos
                            .OrderBy(r => r.Grupo)
                            .ToList();
            ViewBag.Grupos = new SelectList(grupos, "Id", "Grupo");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPublicadoresByGrupo(int id)
        {
            var publicadores = db.Publicadores
                                .Where(p => p.IdGrupo == id)
                                .OrderBy(r => r.Nome)
                                .Select(r => new { r.Nome, r.Id })
                                .ToList();
            return Json(publicadores, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTipoPublicador(int id)
        {
            var idTipo = db.Publicadores
                                .Where(p => p.Id == id)
                                .Select(r => r.IdTipo)
                                .FirstOrDefault();
            return Json(idTipo, JsonRequestBehavior.AllowGet);
        }


        private void LoadTipos()
        {
            var tipos = db.Tipos
                            .OrderBy(r => r.Tipo)
                            .ToList();

            ViewBag.Tipos = new SelectList(tipos, "Id", "Tipo");
        }

        public ActionResult GetRelatorioPublicador(int idPublicador, int ano, int mes)
        {
            var anoMes = ano.ToString() + mes.ToString().PadLeft(2, '0');
            var rel = db.Relatorios
                        .Where(r => r.IdPublicador == idPublicador && r.AnoMes == anoMes)
                        .Select(r => r)
                        .FirstOrDefault();
            if (rel == null)
            {
                rel = new Relatorios();
                rel.IdTipo = db.Publicadores.Where(p => p.Id == idPublicador).Select(r => r.IdTipo).FirstOrDefault();
                rel.Ano = ano;
                rel.Mes = mes;
            }
            else
            {
                rel.Mes = int.Parse(rel.AnoMes.Substring(4, 2));
                rel.Ano = int.Parse(rel.AnoMes.Substring(0, 4));
            }
            var result = new
            {
                IdTipo = rel.IdTipo,
                Horas = rel.Horas,
                rel.Publicacoes,
                rel.Videos,
                rel.Revisitas,
                rel.Estudos
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}