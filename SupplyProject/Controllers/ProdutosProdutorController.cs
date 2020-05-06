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
    public class ProdutosProdutorController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: ProdutosProdutor
        public ActionResult Index()
        {
            var produto_produtor = db.Produto_produtor.Include(p => p.Produtor);
            return View(produto_produtor.ToList());
        }

        // GET: ProdutosProdutor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto_produtor produto_produtor = db.Produto_produtor.Find(id);
            if (produto_produtor == null)
            {
                return HttpNotFound();
            }
            return View(produto_produtor);
        }

        // GET: ProdutosProdutor/Create
        public ActionResult Create()
        {
            ViewBag.Produtor_idProdutor = new SelectList(db.Produtor, "idProdutor", "nome_produtor");
            return View();
        }

        // POST: ProdutosProdutor/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProduto_produtor,nome_prodP,preco_prodP,peso_prodP,largura_prodP,altura_prodP,profundidade_prodP,quantidade_prodP,tempo_producaoP,Produtor_idProdutor")] Produto_produtor produto_produtor)
        {
            if (ModelState.IsValid)
            {
                db.Produto_produtor.Add(produto_produtor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Produtor_idProdutor = new SelectList(db.Produtor, "idProdutor", "nome_produtor", produto_produtor.Produtor_idProdutor);
            return View(produto_produtor);
        }

        // GET: ProdutosProdutor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto_produtor produto_produtor = db.Produto_produtor.Find(id);
            if (produto_produtor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Produtor_idProdutor = new SelectList(db.Produtor, "idProdutor", "nome_produtor", produto_produtor.Produtor_idProdutor);
            return View(produto_produtor);
        }

        // POST: ProdutosProdutor/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProduto_produtor,nome_prodP,preco_prodP,peso_prodP,largura_prodP,altura_prodP,profundidade_prodP,quantidade_prodP,tempo_producaoP,Produtor_idProdutor")] Produto_produtor produto_produtor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto_produtor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Produtor_idProdutor = new SelectList(db.Produtor, "idProdutor", "nome_produtor", produto_produtor.Produtor_idProdutor);
            return View(produto_produtor);
        }

        // GET: ProdutosProdutor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto_produtor produto_produtor = db.Produto_produtor.Find(id);
            if (produto_produtor == null)
            {
                return HttpNotFound();
            }
            return View(produto_produtor);
        }

        // POST: ProdutosProdutor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto_produtor produto_produtor = db.Produto_produtor.Find(id);
            db.Produto_produtor.Remove(produto_produtor);
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
