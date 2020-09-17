using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SupplyProject.Models;

namespace SupplyProject.Controllers
{
    public class ArmazensController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: Armazens
        public ActionResult Index()
        {
            return View(db.Armazem.ToList());
        }

        public ActionResult ExibirEstatisticas()
        {
            return View();
        }

        // GET: Armazens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Armazem armazem = db.Armazem.Find(id);
            if (armazem == null)
            {
                return HttpNotFound();
            }
            return View(armazem);
        }

        // GET: Armazens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Armazens/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idArmazem,nome_armazem,logradouro_armazem,numlogradouro_armazem,largura_armazem,altura_armazem,profundidade_armazem,telefone_armazem")] Armazem armazem)
        {
            if (ModelState.IsValid)
            {
                db.Armazem.Add(armazem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(armazem);
        }

        // GET: Armazens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Armazem armazem = db.Armazem.Find(id);
            if (armazem == null)
            {
                return HttpNotFound();
            }
            return View(armazem);
        }

        // POST: Armazens/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idArmazem,nome_armazem,logradouro_armazem,numlogradouro_armazem,largura_armazem,altura_armazem,profundidade_armazem,telefone_armazem")] Armazem armazem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(armazem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(armazem);
        }

        // GET: Armazens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Armazem armazem = db.Armazem.Find(id);
            if (armazem == null)
            {
                return HttpNotFound();
            }
            return View(armazem);
        }

        // POST: Armazens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Armazem armazem = db.Armazem.Find(id);
            db.Armazem.Remove(armazem);
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
