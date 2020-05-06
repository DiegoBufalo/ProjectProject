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
    public class PedidosUsuarioController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: PedidosUsuario
        public ActionResult Index()
        {
            var pedido_usuario = db.Pedido_usuario.Include(p => p.Armazem).Include(p => p.Produto_fornecedor).Include(p => p.Usuario);
            return View(pedido_usuario.ToList());
        }

        // GET: PedidosUsuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido_usuario pedido_usuario = db.Pedido_usuario.Find(id);
            if (pedido_usuario == null)
            {
                return HttpNotFound();
            }
            return View(pedido_usuario);
        }

        // GET: PedidosUsuario/Create
        public ActionResult Create()
        {
            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem");
            ViewBag.Produto_fornecedor_idProduto_fornecedor = new SelectList(db.Produto_fornecedor, "idProduto_fornecedor", "nome_prodF");
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario");
            return View();
        }

        // POST: PedidosUsuario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPedido,Usuario_idUsuario,Produto_fornecedor_idProduto_fornecedor,Armazem_idArmazem,preco_pedido,ano_pedido,mes_pedido,dia_pedido")] Pedido_usuario pedido_usuario)
        {
            if (ModelState.IsValid)
            {
                db.Pedido_usuario.Add(pedido_usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem", pedido_usuario.Armazem_idArmazem);
            ViewBag.Produto_fornecedor_idProduto_fornecedor = new SelectList(db.Produto_fornecedor, "idProduto_fornecedor", "nome_prodF", pedido_usuario.Produto_fornecedor_idProduto_fornecedor);
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", pedido_usuario.Usuario_idUsuario);
            return View(pedido_usuario);
        }

        // GET: PedidosUsuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido_usuario pedido_usuario = db.Pedido_usuario.Find(id);
            if (pedido_usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem", pedido_usuario.Armazem_idArmazem);
            ViewBag.Produto_fornecedor_idProduto_fornecedor = new SelectList(db.Produto_fornecedor, "idProduto_fornecedor", "nome_prodF", pedido_usuario.Produto_fornecedor_idProduto_fornecedor);
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", pedido_usuario.Usuario_idUsuario);
            return View(pedido_usuario);
        }

        // POST: PedidosUsuario/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPedido,Usuario_idUsuario,Produto_fornecedor_idProduto_fornecedor,Armazem_idArmazem,preco_pedido,ano_pedido,mes_pedido,dia_pedido")] Pedido_usuario pedido_usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido_usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem", pedido_usuario.Armazem_idArmazem);
            ViewBag.Produto_fornecedor_idProduto_fornecedor = new SelectList(db.Produto_fornecedor, "idProduto_fornecedor", "nome_prodF", pedido_usuario.Produto_fornecedor_idProduto_fornecedor);
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", pedido_usuario.Usuario_idUsuario);
            return View(pedido_usuario);
        }

        // GET: PedidosUsuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido_usuario pedido_usuario = db.Pedido_usuario.Find(id);
            if (pedido_usuario == null)
            {
                return HttpNotFound();
            }
            return View(pedido_usuario);
        }

        // POST: PedidosUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido_usuario pedido_usuario = db.Pedido_usuario.Find(id);
            db.Pedido_usuario.Remove(pedido_usuario);
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
