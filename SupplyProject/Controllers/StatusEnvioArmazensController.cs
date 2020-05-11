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
    public class StatusEnvioArmazensController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: StatusEnvioArmazens
        public ActionResult Index()
        {
            return View(db.StatusEnvioArmazem.ToList());
        }

        // GET: StatusEnvioArmazens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusEnvioArmazem statusEnvioArmazem = db.StatusEnvioArmazem.Find(id);
            if (statusEnvioArmazem == null)
            {
                return HttpNotFound();
            }
            return View(statusEnvioArmazem);
        }

        // GET: StatusEnvioArmazens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusEnvioArmazens/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idStatusEnvio,statusEnvio")] StatusEnvioArmazem statusEnvioArmazem)
        {
            if (ModelState.IsValid)
            {
                db.StatusEnvioArmazem.Add(statusEnvioArmazem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusEnvioArmazem);
        }

        // GET: StatusEnvioArmazens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusEnvioArmazem statusEnvioArmazem = db.StatusEnvioArmazem.Find(id);
            if (statusEnvioArmazem == null)
            {
                return HttpNotFound();
            }
            return View(statusEnvioArmazem);
        }

        // POST: StatusEnvioArmazens/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idStatusEnvio,statusEnvio")] StatusEnvioArmazem statusEnvioArmazem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusEnvioArmazem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusEnvioArmazem);
        }

        // GET: StatusEnvioArmazens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusEnvioArmazem statusEnvioArmazem = db.StatusEnvioArmazem.Find(id);
            if (statusEnvioArmazem == null)
            {
                return HttpNotFound();
            }
            return View(statusEnvioArmazem);
        }

        // POST: StatusEnvioArmazens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusEnvioArmazem statusEnvioArmazem = db.StatusEnvioArmazem.Find(id);
            db.StatusEnvioArmazem.Remove(statusEnvioArmazem);
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
