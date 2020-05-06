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
    public class ProdutoresController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: Produtores
        public ActionResult Index()
        {
            return View(db.Produtor.ToList());
        }

        // GET: Produtores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtor produtor = db.Produtor.Find(id);
            if (produtor == null)
            {
                return HttpNotFound();
            }
            return View(produtor);
        }

        // GET: Produtores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtores/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProdutor,nome_produtor,logradouro_produtor,numlogradouro_produtor,cnpj_produtor,telefone_produtor,email_produtor")] Produtor produtor)
        {
            if (ModelState.IsValid)
            {
                db.Produtor.Add(produtor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produtor);
        }

        // GET: Produtores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtor produtor = db.Produtor.Find(id);
            if (produtor == null)
            {
                return HttpNotFound();
            }
            return View(produtor);
        }

        // POST: Produtores/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProdutor,nome_produtor,logradouro_produtor,numlogradouro_produtor,cnpj_produtor,telefone_produtor,email_produtor")] Produtor produtor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produtor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produtor);
        }

        // GET: Produtores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtor produtor = db.Produtor.Find(id);
            if (produtor == null)
            {
                return HttpNotFound();
            }
            return View(produtor);
        }

        // POST: Produtores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produtor produtor = db.Produtor.Find(id);
            db.Produtor.Remove(produtor);
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
