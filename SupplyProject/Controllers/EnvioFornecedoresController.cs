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
    public class EnvioFornecedoresController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: EnvioFornecedores
        public ActionResult Index()
        {
            var envioFornecedor = db.EnvioFornecedor.Include(e => e.PedidoFinal_usuario).Include(e => e.StatusEnvioFornecedor).Where(e => e.statusEnvio == 1).Include(e => e.Veiculo);
            return View(envioFornecedor.ToList());
        }
        public ActionResult IndexConcluida()
        {
            var envioFornecedor = db.EnvioFornecedor.Include(e => e.PedidoFinal_usuario).Include(e => e.StatusEnvioFornecedor).Where(e => e.statusEnvio == 2).Include(e => e.Veiculo);
            return View(envioFornecedor.ToList());
        }
       


        // GET: EnvioFornecedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnvioFornecedor envioFornecedor = db.EnvioFornecedor.Find(id);
            if (envioFornecedor == null)
            {
                return HttpNotFound();
            }
            return View(envioFornecedor);
        }


        // GET: EnvioFornecedores/Create
        public ActionResult Create()
        {
            ViewBag.idPedido = new SelectList(db.PedidoFinal_usuario.Where(e => e.statusPedido == 1), "idPedido", "idPedido");
            ViewBag.statusEnvio = new SelectList(db.StatusEnvioFornecedor, "idStatusEnvio", "statusEnvio");
            ViewBag.idVeiculo = new SelectList(db.Veiculo, "idVeiculo", "tipo_veiculo");
            return View();
        }

        // POST: EnvioFornecedores/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEnvio,idPedido,idVeiculo,statusEnvio,ano_envio,mes_envio,dia_envio")] EnvioFornecedor envioFornecedor)
        {
            PedidoFinal_usuario pedidoAtual = db.PedidoFinal_usuario.Find(envioFornecedor.idPedido);
            int quantidadePedido = pedidoAtual.quantidade;
            int produtoPedido = pedidoAtual.Produto_fornecedor_idProduto_fornecedor;
            Produto_armazem prodArmazem = db.Produto_armazem.Find(produtoPedido);
            int quantidadeEstoque = prodArmazem.quantidade_prodA;
            prodArmazem.quantidade_prodA = quantidadeEstoque + quantidadePedido;
            ProdutosArmazemController prodControl = new ProdutosArmazemController();
            int idProduto = prodArmazem.idProduto_armazem;
            prodControl.Edit(idProduto);

            if (ModelState.IsValid)
            {
                db.EnvioFornecedor.Add(envioFornecedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPedido = new SelectList(db.PedidoFinal_usuario, "idPedido", "idPedido", envioFornecedor.idPedido);
            ViewBag.statusEnvio = new SelectList(db.StatusEnvioFornecedor, "idStatusEnvio", "statusEnvio", envioFornecedor.statusEnvio);
            ViewBag.idVeiculo = new SelectList(db.Veiculo, "idVeiculo", "tipo_veiculo", envioFornecedor.idVeiculo);
            return View(envioFornecedor);
        }

        

        // GET: EnvioFornecedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnvioFornecedor envioFornecedor = db.EnvioFornecedor.Find(id);
            if (envioFornecedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPedido = new SelectList(db.PedidoFinal_usuario, "idPedido", "idPedido", envioFornecedor.idPedido);
            ViewBag.statusEnvio = new SelectList(db.StatusEnvioFornecedor, "idStatusEnvio", "statusEnvio", envioFornecedor.statusEnvio);
            ViewBag.idVeiculo = new SelectList(db.Veiculo, "idVeiculo", "tipo_veiculo", envioFornecedor.idVeiculo);
            return View(envioFornecedor);
        }

        // POST: EnvioFornecedores/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEnvio,idPedido,idVeiculo,statusEnvio,ano_envio,mes_envio,dia_envio")] EnvioFornecedor envioFornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(envioFornecedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int idPedido = envioFornecedor.idPedido;
            PedidoFinal_usuario pedido = db.PedidoFinal_usuario.Find(idPedido);
            pedido.statusPedido = 2;
            PedidoFinalUsuarioController pedidoConcluido = new PedidoFinalUsuarioController();
            pedidoConcluido.Edit(pedido);
            

            ViewBag.idPedido = new SelectList(db.PedidoFinal_usuario, "idPedido", "idPedido", envioFornecedor.idPedido);
            ViewBag.statusEnvio = new SelectList(db.StatusEnvioFornecedor, "idStatusEnvio", "statusEnvio", envioFornecedor.statusEnvio);
            ViewBag.idVeiculo = new SelectList(db.Veiculo, "idVeiculo", "tipo_veiculo", envioFornecedor.idVeiculo);
            return View(envioFornecedor);
        }

        // GET: EnvioFornecedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnvioFornecedor envioFornecedor = db.EnvioFornecedor.Find(id);
            if (envioFornecedor == null)
            {
                return HttpNotFound();
            }
            return View(envioFornecedor);
        }

        // POST: EnvioFornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnvioFornecedor envioFornecedor = db.EnvioFornecedor.Find(id);
            db.EnvioFornecedor.Remove(envioFornecedor);
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
