using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RelatoriosCongregação.Models;

namespace RelatoriosCongregação.Controllers.Cadastros
{
    public class PublicadoresController : Controller
    {
        private FieldServiceEntities db = new FieldServiceEntities();


        // GET: Publicadores
        public ActionResult Index(string search)
        {
            var publicadores = db.Publicadores.Include(p => p.Grupo).Include(p => p.Tipos);
            if (!string.IsNullOrWhiteSpace(search))
            {
                publicadores = publicadores.Where(p => p.Nome.Contains(search) || p.Grupo.Grupo.Contains(search));
            }
            return View(publicadores.OrderBy(p => p.Nome).ToList());
        }


        // GET: Publicadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicadores publicadores = db.Publicadores.Find(id);
            if (publicadores == null)
            {
                return HttpNotFound();
            }
            return View(publicadores);
        }

        // GET: Publicadores/Create
        public ActionResult Create()
        {
            ViewBag.IdGrupo = new SelectList(db.Grupos, "Id", "Grupo");
            ViewBag.IdTipo = new SelectList(db.Tipos, "Id", "Tipo");
            ViewBag.Ativo = true;
            return View();
        }

        // POST: Publicadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,IdGrupo,IdTipo")] Publicadores publicadores)
        {
            if (ModelState.IsValid)
            {
                publicadores.Id = db.Publicadores.Max(p => p.Id) + 1;
                publicadores.Ativo = true;
                db.Publicadores.Add(publicadores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdGrupo = new SelectList(db.Grupos, "Id", "Grupo", publicadores.IdGrupo);
            ViewBag.IdTipo = new SelectList(db.Tipos, "Id", "Tipo", publicadores.IdTipo);
            return View(publicadores);
        }

        // GET: Publicadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicadores publicadores = db.Publicadores.Find(id);
            if (publicadores == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGrupo = new SelectList(db.Grupos, "Id", "Grupo", publicadores.IdGrupo);
            ViewBag.IdTipo = new SelectList(db.Tipos, "Id", "Tipo", publicadores.IdTipo);
            return View(publicadores);
        }

        // POST: Publicadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,IdGrupo,IdTipo,Ativo")] Publicadores publicadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publicadores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdGrupo = new SelectList(db.Grupos, "Id", "Grupo", publicadores.IdGrupo);
            ViewBag.IdTipo = new SelectList(db.Tipos, "Id", "Tipo", publicadores.IdTipo);
            return View(publicadores);
        }

        // GET: Publicadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicadores publicadores = db.Publicadores.Find(id);
            if (publicadores == null)
            {
                return HttpNotFound();
            }
            return View(publicadores);
        }

        // POST: Publicadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publicadores publicadores = db.Publicadores.Find(id);
            db.Publicadores.Remove(publicadores);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
