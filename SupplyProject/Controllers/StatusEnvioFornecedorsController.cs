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
    public class StatusEnvioFornecedorsController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: StatusEnvioFornecedors
        public ActionResult Index()
        {
            return View(db.StatusEnvioFornecedor.ToList());
        }

        // GET: StatusEnvioFornecedors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusEnvioFornecedor statusEnvioFornecedor = db.StatusEnvioFornecedor.Find(id);
            if (statusEnvioFornecedor == null)
            {
                return HttpNotFound();
            }
            return View(statusEnvioFornecedor);
        }

        // GET: StatusEnvioFornecedors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusEnvioFornecedors/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idStatusEnvio,statusEnvio")] StatusEnvioFornecedor statusEnvioFornecedor)
        {
            if (ModelState.IsValid)
            {
                db.StatusEnvioFornecedor.Add(statusEnvioFornecedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusEnvioFornecedor);
        }

        // GET: StatusEnvioFornecedors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusEnvioFornecedor statusEnvioFornecedor = db.StatusEnvioFornecedor.Find(id);
            if (statusEnvioFornecedor == null)
            {
                return HttpNotFound();
            }
            return View(statusEnvioFornecedor);
        }

        // POST: StatusEnvioFornecedors/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idStatusEnvio,statusEnvio")] StatusEnvioFornecedor statusEnvioFornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusEnvioFornecedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusEnvioFornecedor);
        }

        // GET: StatusEnvioFornecedors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusEnvioFornecedor statusEnvioFornecedor = db.StatusEnvioFornecedor.Find(id);
            if (statusEnvioFornecedor == null)
            {
                return HttpNotFound();
            }
            return View(statusEnvioFornecedor);
        }

        // POST: StatusEnvioFornecedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusEnvioFornecedor statusEnvioFornecedor = db.StatusEnvioFornecedor.Find(id);
            db.StatusEnvioFornecedor.Remove(statusEnvioFornecedor);
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
