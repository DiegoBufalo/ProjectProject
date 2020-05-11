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
    public class ProdutosArmazemController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: ProdutosArmazem
        public ActionResult Index()
        {
            var produto_armazem = db.Produto_armazem.Include(p => p.Armazem).Include(p => p.Usuario);
            return View(produto_armazem.ToList());
        }

        public ActionResult ExibirEstatisticas()
        {
            return View();
        }

        // GET: ProdutosArmazem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto_armazem produto_armazem = db.Produto_armazem.Find(id);
            if (produto_armazem == null)
            {
                return HttpNotFound();
            }
            return View(produto_armazem);
        }

        // GET: ProdutosArmazem/Create
        public ActionResult Create()
        {
            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem");
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario");
            return View();
        }

        // POST: ProdutosArmazem/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProduto_armazem,nome_prodA,preco_prodA,peso_prodA,largura_prodA,altura_prodA,profundidade_prodA,quantidade_prodA,Usuario_idUsuario,Armazem_idArmazem")] Produto_armazem produto_armazem)
        {
            if (ModelState.IsValid)
            {
                db.Produto_armazem.Add(produto_armazem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem", produto_armazem.Armazem_idArmazem);
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", produto_armazem.Usuario_idUsuario);
            return View(produto_armazem);
        }

        // GET: ProdutosArmazem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto_armazem produto_armazem = db.Produto_armazem.Find(id);
            if (produto_armazem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem", produto_armazem.Armazem_idArmazem);
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", produto_armazem.Usuario_idUsuario);
            return View(produto_armazem);
        }

        // POST: ProdutosArmazem/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProduto_armazem,nome_prodA,preco_prodA,peso_prodA,largura_prodA,altura_prodA,profundidade_prodA,quantidade_prodA,Usuario_idUsuario,Armazem_idArmazem")] Produto_armazem produto_armazem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto_armazem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem", produto_armazem.Armazem_idArmazem);
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", produto_armazem.Usuario_idUsuario);
            return View(produto_armazem);
        }

       public ActionResult AtulizaArmazem(Produto_armazem produto)
        {
            if (ModelState.IsValid)
            {
               db.Entry(produto).State = EntityState.Modified;
               db.SaveChanges();
            }

           return View("IndexEncerrado", "PedidoFinalUsuario");

       }

        // GET: ProdutosArmazem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto_armazem produto_armazem = db.Produto_armazem.Find(id);
            if (produto_armazem == null)
            {
                return HttpNotFound();
            }
            return View(produto_armazem);
        }

        // POST: ProdutosArmazem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto_armazem produto_armazem = db.Produto_armazem.Find(id);
            db.Produto_armazem.Remove(produto_armazem);
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
