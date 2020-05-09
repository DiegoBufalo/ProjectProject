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
    public class StatusPedidoesController : Controller
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: StatusPedidoes
        public ActionResult Index()
        {
            return View(db.StatusPedido.ToList());
        }

        // GET: StatusPedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusPedido statusPedido = db.StatusPedido.Find(id);
            if (statusPedido == null)
            {
                return HttpNotFound();
            }
            return View(statusPedido);
        }

        // GET: StatusPedidoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusPedidoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idStatus,nome_status")] StatusPedido statusPedido)
        {
            if (ModelState.IsValid)
            {
                db.StatusPedido.Add(statusPedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusPedido);
        }

        // GET: StatusPedidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusPedido statusPedido = db.StatusPedido.Find(id);
            if (statusPedido == null)
            {
                return HttpNotFound();
            }
            return View(statusPedido);
        }

        // POST: StatusPedidoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idStatus,nome_status")] StatusPedido statusPedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusPedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusPedido);
        }

        // GET: StatusPedidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusPedido statusPedido = db.StatusPedido.Find(id);
            if (statusPedido == null)
            {
                return HttpNotFound();
            }
            return View(statusPedido);
        }

        // POST: StatusPedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusPedido statusPedido = db.StatusPedido.Find(id);
            db.StatusPedido.Remove(statusPedido);
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
