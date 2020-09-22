using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SupplyProject.Models;
using SupplyProject.Services;

namespace SupplyProject.Controllers
{
    public class DemandaFinalProdutorController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: DemandaFinalProdutor
        public ActionResult Index()
        {
            

            var demandaFinal_produtor = db.DemandaFinal_produtor.Include(d => d.Usuario).Include(d => d.Produto_armazem).Include(d => d.Produto_produtor).Include(d => d.StatusDemanda).Where(d => d.status_demanda == 1) ;
            return View(demandaFinal_produtor.ToList());
        }
        public ActionResult IndexEncerrada()
        {
            var demandaFinal_produtor = db.DemandaFinal_produtor.Include(d => d.Usuario).Include(d => d.Produto_armazem).Include(d => d.Produto_produtor).Include(d => d.StatusDemanda).Where(d => d.status_demanda == 2);
            return View(demandaFinal_produtor.ToList());
        }

        public ActionResult IndexSupply()
        {
            int id = UsuarioService.VerificaSeOUsuarioEstaLogado().idUsuario;

            var demandaFinal_produtor = db.DemandaFinal_produtor.Include(d => d.Usuario).Where(d => d.Usuario_idUsuario == id).Include(d => d.Produto_armazem).Include(d => d.Produto_produtor).Include(d => d.StatusDemanda).Where(d => d.status_demanda == 1);
            return View(demandaFinal_produtor.ToList());
        }

        public ActionResult ExibirEstatisticas()
        {
            return View();
        }



        // GET: DemandaFinalProdutor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandaFinal_produtor demandaFinal_produtor = db.DemandaFinal_produtor.Find(id);
            if (demandaFinal_produtor == null)
            {
                return HttpNotFound();
            }
            return View(demandaFinal_produtor);
        }

        public ActionResult DetailsEncerrada(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandaFinal_produtor demandaFinal_produtor = db.DemandaFinal_produtor.Find(id);
            if (demandaFinal_produtor == null)
            {
                return HttpNotFound();
            }
            return View(demandaFinal_produtor);
        }

        // GET: DemandaFinalProdutor/Create
        public ActionResult Create()
        {
            
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario");
            ViewBag.Produto_armazem_idProduto_armazem = new SelectList(db.Produto_armazem, "idProduto_armazem", "nome_prodA","Usuario_idUsuario");
            ViewBag.Produto_produtor_idProduto_produtor = new SelectList(db.Produto_produtor, "idProduto_produtor", "nome_prodP");
            ViewBag.status_demanda = new SelectList(db.StatusDemanda, "idDemandaFinal", "nome_status");
            return View();
        }

        // POST: DemandaFinalProdutor/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDemandaFinal,Produto_produtor_idProduto_produtor,Produto_armazem_idProduto_armazem,ano_pedido,mes_pedido,dia_pedido,quantidade")] DemandaFinal_produtor demandaFinal_produtor)
        {
            Produto_armazem prodarm = db.Produto_armazem.Find(demandaFinal_produtor.Produto_armazem_idProduto_armazem);
            int idUser = prodarm.Usuario_idUsuario;
            Usuario userResp = db.Usuario.Find(idUser);
            demandaFinal_produtor.Usuario_idUsuario = userResp.idUsuario;
            demandaFinal_produtor.status_demanda = 1;

            if (ModelState.IsValid)
            {
                db.DemandaFinal_produtor.Add(demandaFinal_produtor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", demandaFinal_produtor.Usuario_idUsuario);
            ViewBag.Produto_armazem_idProduto_armazem = new SelectList(db.Produto_armazem, "idProduto_armazem", "nome_prodA", demandaFinal_produtor.Produto_armazem_idProduto_armazem);
            ViewBag.Produto_produtor_idProduto_produtor = new SelectList(db.Produto_produtor, "idProduto_produtor", "nome_prodP", demandaFinal_produtor.Produto_produtor_idProduto_produtor);
            ViewBag.status_demanda = new SelectList(db.StatusDemanda, "idDemandaFinal", "nome_status", demandaFinal_produtor.status_demanda);
            return View(demandaFinal_produtor);
        }

        // GET: DemandaFinalProdutor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandaFinal_produtor demandaFinal_produtor = db.DemandaFinal_produtor.Find(id);
            if (demandaFinal_produtor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", demandaFinal_produtor.Usuario_idUsuario);
            ViewBag.Produto_armazem_idProduto_armazem = new SelectList(db.Produto_armazem, "idProduto_armazem", "nome_prodA", demandaFinal_produtor.Produto_armazem_idProduto_armazem);
            ViewBag.Produto_produtor_idProduto_produtor = new SelectList(db.Produto_produtor, "idProduto_produtor", "nome_prodP", demandaFinal_produtor.Produto_produtor_idProduto_produtor);
            ViewBag.status_demanda = new SelectList(db.StatusDemanda, "idDemandaFinal", "nome_status", demandaFinal_produtor.status_demanda);
            return View(demandaFinal_produtor);
        }

        // POST: DemandaFinalProdutor/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDemandaFinal,Produto_produtor_idProduto_produtor,Produto_armazem_idProduto_armazem,Usuario_idUsuario,status_demanda,ano_pedido,mes_pedido,dia_pedido,quantidade")] DemandaFinal_produtor demandaFinal_produtor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demandaFinal_produtor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario, "idUsuario", "nome_usuario", demandaFinal_produtor.Usuario_idUsuario);
            ViewBag.Produto_armazem_idProduto_armazem = new SelectList(db.Produto_armazem, "idProduto_armazem", "nome_prodA", demandaFinal_produtor.Produto_armazem_idProduto_armazem);
            ViewBag.Produto_produtor_idProduto_produtor = new SelectList(db.Produto_produtor, "idProduto_produtor", "nome_prodP", demandaFinal_produtor.Produto_produtor_idProduto_produtor);
            ViewBag.status_demanda = new SelectList(db.StatusDemanda, "idDemandaFinal", "nome_status", demandaFinal_produtor.status_demanda);
            return View(demandaFinal_produtor);
        }

        // GET: DemandaFinalProdutor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandaFinal_produtor demandaFinal_produtor = db.DemandaFinal_produtor.Find(id);
            if (demandaFinal_produtor == null)
            {
                return HttpNotFound();
            }
            return View(demandaFinal_produtor);
        }

        // POST: DemandaFinalProdutor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DemandaFinal_produtor demandaFinal_produtor = db.DemandaFinal_produtor.Find(id);
            db.DemandaFinal_produtor.Remove(demandaFinal_produtor);
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
