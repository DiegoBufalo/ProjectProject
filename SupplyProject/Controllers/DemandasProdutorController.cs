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
    public class DemandasProdutorController : Controller
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: DemandasProdutor
        public ActionResult Index()
        {
            var demanda_produtor = db.Demanda_produtor.Include(d => d.Usuario).Include(d => d.Produto_armazem).Include(d => d.Produto_produtor);
            return View(demanda_produtor.ToList());
        }

        // GET: DemandasProdutor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demanda_produtor demanda_produtor = db.Demanda_produtor.Find(id);
            if (demanda_produtor == null)
            {
                return HttpNotFound();
            }
            return View(demanda_produtor);
        }

        // GET: DemandasProdutor/Create
        public ActionResult Create()
        {
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario");
            ViewBag.Produto_armazem_idProduto_armazem = new SelectList(db.Produto_armazem, "idProduto_armazem", "nome_prodA");
            ViewBag.Produto_produtor_idProduto_produtor = new SelectList(db.Produto_produtor, "idProduto_produtor", "nome_prodP");
            return View();
        }

        // POST: DemandasProdutor/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDemanda,Produto_produtor_idProduto_produtor,Produto_armazem_idProduto_armazem,Usuario_idUsuario,ano_pedido,mes_pedido,dia_pedido")] Demanda_produtor demanda_produtor)
        {
            if (ModelState.IsValid)
            {
                db.Demanda_produtor.Add(demanda_produtor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", demanda_produtor.Usuario_idUsuario);
            ViewBag.Produto_armazem_idProduto_armazem = new SelectList(db.Produto_armazem, "idProduto_armazem", "nome_prodA", demanda_produtor.Produto_armazem_idProduto_armazem);
            ViewBag.Produto_produtor_idProduto_produtor = new SelectList(db.Produto_produtor, "idProduto_produtor", "nome_prodP", demanda_produtor.Produto_produtor_idProduto_produtor);
            return View(demanda_produtor);
        }

        // GET: DemandasProdutor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demanda_produtor demanda_produtor = db.Demanda_produtor.Find(id);
            if (demanda_produtor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", demanda_produtor.Usuario_idUsuario);
            ViewBag.Produto_armazem_idProduto_armazem = new SelectList(db.Produto_armazem, "idProduto_armazem", "nome_prodA", demanda_produtor.Produto_armazem_idProduto_armazem);
            ViewBag.Produto_produtor_idProduto_produtor = new SelectList(db.Produto_produtor, "idProduto_produtor", "nome_prodP", demanda_produtor.Produto_produtor_idProduto_produtor);
            return View(demanda_produtor);
        }

        // POST: DemandasProdutor/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDemanda,Produto_produtor_idProduto_produtor,Produto_armazem_idProduto_armazem,Usuario_idUsuario,ano_pedido,mes_pedido,dia_pedido")] Demanda_produtor demanda_produtor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demanda_produtor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", demanda_produtor.Usuario_idUsuario);
            ViewBag.Produto_armazem_idProduto_armazem = new SelectList(db.Produto_armazem, "idProduto_armazem", "nome_prodA", demanda_produtor.Produto_armazem_idProduto_armazem);
            ViewBag.Produto_produtor_idProduto_produtor = new SelectList(db.Produto_produtor, "idProduto_produtor", "nome_prodP", demanda_produtor.Produto_produtor_idProduto_produtor);
            return View(demanda_produtor);
        }

        // GET: DemandasProdutor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demanda_produtor demanda_produtor = db.Demanda_produtor.Find(id);
            if (demanda_produtor == null)
            {
                return HttpNotFound();
            }
            return View(demanda_produtor);
        }

        // POST: DemandasProdutor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Demanda_produtor demanda_produtor = db.Demanda_produtor.Find(id);
            db.Demanda_produtor.Remove(demanda_produtor);
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
