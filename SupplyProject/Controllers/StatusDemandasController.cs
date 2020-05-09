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
    public class StatusDemandasController : Controller
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: StatusDemandas
        public ActionResult Index()
        {
            return View(db.StatusDemanda.ToList());
        }

        // GET: StatusDemandas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusDemanda statusDemanda = db.StatusDemanda.Find(id);
            if (statusDemanda == null)
            {
                return HttpNotFound();
            }
            return View(statusDemanda);
        }

        // GET: StatusDemandas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusDemandas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDemandaFinal,nome_status")] StatusDemanda statusDemanda)
        {
            if (ModelState.IsValid)
            {
                db.StatusDemanda.Add(statusDemanda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusDemanda);
        }

        // GET: StatusDemandas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusDemanda statusDemanda = db.StatusDemanda.Find(id);
            if (statusDemanda == null)
            {
                return HttpNotFound();
            }
            return View(statusDemanda);
        }

        // POST: StatusDemandas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDemandaFinal,nome_status")] StatusDemanda statusDemanda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusDemanda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusDemanda);
        }

        // GET: StatusDemandas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusDemanda statusDemanda = db.StatusDemanda.Find(id);
            if (statusDemanda == null)
            {
                return HttpNotFound();
            }
            return View(statusDemanda);
        }

        // POST: StatusDemandas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusDemanda statusDemanda = db.StatusDemanda.Find(id);
            db.StatusDemanda.Remove(statusDemanda);
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
