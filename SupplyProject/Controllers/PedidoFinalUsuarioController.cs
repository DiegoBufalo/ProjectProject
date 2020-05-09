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
    public class PedidoFinalUsuarioController : Controller
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: PedidoFinalUsuario
        public ActionResult Index()
        {
            var pedidoFinal_usuario = db.PedidoFinal_usuario.Include(p => p.Armazem).Include(p => p.Produto_fornecedor).Include(p => p.StatusPedido1).Where(p => p.statusPedido ==1).Include(p => p.Usuario);
            return View(pedidoFinal_usuario.ToList());
        }

        public ActionResult IndexEncerrado()
        {
            var pedidoFinal_usuario = db.PedidoFinal_usuario.Include(p => p.Armazem).Include(p => p.Produto_fornecedor).Include(p => p.StatusPedido1).Where(p => p.statusPedido == 2).Include(p => p.Usuario);
            return View(pedidoFinal_usuario.ToList());
        }
        // GET: PedidoFinalUsuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoFinal_usuario pedidoFinal_usuario = db.PedidoFinal_usuario.Find(id);
            if (pedidoFinal_usuario == null)
            {
                return HttpNotFound();
            }
            return View(pedidoFinal_usuario);
        }

        // GET: PedidoFinalUsuario/Create
        public ActionResult Create()
        {
            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem");
            ViewBag.Produto_fornecedor_idProduto_fornecedor = new SelectList(db.Produto_fornecedor, "idProduto_fornecedor", "nome_prodF");
            ViewBag.statusPedido = new SelectList(db.StatusPedido, "idStatus", "nome_status");
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario");
            return View();
        }

        // POST: PedidoFinalUsuario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPedido,Usuario_idUsuario,Produto_fornecedor_idProduto_fornecedor,Armazem_idArmazem,statusPedido,preco_pedido,ano_pedido,mes_pedido,dia_pedido")] PedidoFinal_usuario pedidoFinal_usuario)
        {
            if (ModelState.IsValid)
            {
                db.PedidoFinal_usuario.Add(pedidoFinal_usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem", pedidoFinal_usuario.Armazem_idArmazem);
            ViewBag.Produto_fornecedor_idProduto_fornecedor = new SelectList(db.Produto_fornecedor, "idProduto_fornecedor", "nome_prodF", pedidoFinal_usuario.Produto_fornecedor_idProduto_fornecedor);
            ViewBag.statusPedido = new SelectList(db.StatusPedido, "idStatus", "nome_status", pedidoFinal_usuario.statusPedido);
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", pedidoFinal_usuario.Usuario_idUsuario);
            return View(pedidoFinal_usuario);
        }

        // GET: PedidoFinalUsuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoFinal_usuario pedidoFinal_usuario = db.PedidoFinal_usuario.Find(id);
            if (pedidoFinal_usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem", pedidoFinal_usuario.Armazem_idArmazem);
            ViewBag.Produto_fornecedor_idProduto_fornecedor = new SelectList(db.Produto_fornecedor, "idProduto_fornecedor", "nome_prodF", pedidoFinal_usuario.Produto_fornecedor_idProduto_fornecedor);
            ViewBag.statusPedido = new SelectList(db.StatusPedido, "idStatus", "nome_status", pedidoFinal_usuario.statusPedido);
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", pedidoFinal_usuario.Usuario_idUsuario);
            return View(pedidoFinal_usuario);
        }

        // POST: PedidoFinalUsuario/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPedido,Usuario_idUsuario,Produto_fornecedor_idProduto_fornecedor,Armazem_idArmazem,statusPedido,preco_pedido,ano_pedido,mes_pedido,dia_pedido")] PedidoFinal_usuario pedidoFinal_usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidoFinal_usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem", pedidoFinal_usuario.Armazem_idArmazem);
            ViewBag.Produto_fornecedor_idProduto_fornecedor = new SelectList(db.Produto_fornecedor, "idProduto_fornecedor", "nome_prodF", pedidoFinal_usuario.Produto_fornecedor_idProduto_fornecedor);
            ViewBag.statusPedido = new SelectList(db.StatusPedido, "idStatus", "nome_status", pedidoFinal_usuario.statusPedido);
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", pedidoFinal_usuario.Usuario_idUsuario);
            return View(pedidoFinal_usuario);
        }

        // GET: PedidoFinalUsuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoFinal_usuario pedidoFinal_usuario = db.PedidoFinal_usuario.Find(id);
            if (pedidoFinal_usuario == null)
            {
                return HttpNotFound();
            }
            return View(pedidoFinal_usuario);
        }

        // POST: PedidoFinalUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidoFinal_usuario pedidoFinal_usuario = db.PedidoFinal_usuario.Find(id);
            db.PedidoFinal_usuario.Remove(pedidoFinal_usuario);
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
