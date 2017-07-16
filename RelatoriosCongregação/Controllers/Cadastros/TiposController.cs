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
    public class TiposController : Controller
    {
        private FieldServiceEntities db = new FieldServiceEntities();

        // GET: Tipos
        public ActionResult Index()
        {
            return View(db.Tipos.ToList());
        }

        // GET: Tipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipos tipos = db.Tipos.Find(id);
            if (tipos == null)
            {
                return HttpNotFound();
            }
            return View(tipos);
        }

        // GET: Tipos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tipo")] Tipos tipos)
        {
            if (ModelState.IsValid)
            {
                tipos.Id = db.Tipos.Max(t => t.Id) + 1;
                db.Tipos.Add(tipos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipos);
        }

        // GET: Tipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipos tipos = db.Tipos.Find(id);
            if (tipos == null)
            {
                return HttpNotFound();
            }
            return View(tipos);
        }

        // POST: Tipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tipo")] Tipos tipos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipos);
        }

        // GET: Tipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipos tipos = db.Tipos.Find(id);
            if (tipos == null)
            {
                return HttpNotFound();
            }
            return View(tipos);
        }

        // POST: Tipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipos tipos = db.Tipos.Find(id);
            db.Tipos.Remove(tipos);
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
