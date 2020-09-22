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
    public class EnvioArmarazemController : Controller
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: EnvioArmarazem
        public ActionResult Index()
        {
            var envioArmarazem = db.EnvioArmarazem.Include(e => e.DemandaFinal_produtor).Include(e => e.Veiculo).Include(e => e.StatusEnvioArmazem).Where(e => e.statusEnvio == 1);
            return View(envioArmarazem.ToList());
        }

        public ActionResult IndexEncerrada()
        {
            var envioArmarazem = db.EnvioArmarazem.Include(e => e.DemandaFinal_produtor).Include(e => e.Veiculo).Include(e => e.StatusEnvioArmazem).Where(e => e.statusEnvio == 2);
            return View(envioArmarazem.ToList());
        }

        // GET: EnvioArmarazem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnvioArmarazem envioArmarazem = db.EnvioArmarazem.Find(id);
            if (envioArmarazem == null)
            {
                return HttpNotFound();
            }
            return View(envioArmarazem);
        }

        // GET: EnvioArmarazem/Create
        public ActionResult Create()
        {
            ViewBag.idDemanda = new SelectList(db.DemandaFinal_produtor.Where(e => e.status_demanda == 1), "idDemandaFinal", "idDemandaFinal");
            ViewBag.idVeiculo = new SelectList(db.Veiculo, "idVeiculo", "placa_veiculo");
            ViewBag.statusEnvio = new SelectList(db.StatusEnvioArmazem, "idStatusEnvio", "statusEnvio");
            return View();
        }

        // POST: EnvioArmarazem/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEnvio,idDemanda,idVeiculo,ano_envio,mes_envio,dia_envio")] EnvioArmarazem envioArmarazem)
        {
            DemandaFinal_produtor demanda = db.DemandaFinal_produtor.Find(envioArmarazem.idDemanda);
            int idProduto = demanda.Produto_armazem_idProduto_armazem;
            Produto_armazem prodArmazem = db.Produto_armazem.Find(idProduto);
            int qtdDemanda = demanda.quantidade;
            int qtdEstoque = prodArmazem.quantidade_prodA;
            prodArmazem.quantidade_prodA = qtdEstoque - qtdDemanda;
            ProdutosArmazemController prodControl = new ProdutosArmazemController();
            int idProd = prodArmazem.idProduto_armazem;
            prodControl.Atualizar(idProd);

            envioArmarazem.statusEnvio = 1;

            if (envioArmarazem.statusEnvio == 1)
            {
                DemandaFinal_produtor demandaAberta = db.DemandaFinal_produtor.Find(envioArmarazem.idDemanda);
                demandaAberta.status_demanda = 3;
                DemandaFinalProdutorController demandaController = new DemandaFinalProdutorController();
                demandaController.Edit(demandaAberta.idDemandaFinal);
            }

            if (ModelState.IsValid)
            {
                db.EnvioArmarazem.Add(envioArmarazem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDemanda = new SelectList(db.DemandaFinal_produtor, "idDemandaFinal", "idDemandaFinal", envioArmarazem.idDemanda);
            ViewBag.idVeiculo = new SelectList(db.Veiculo, "idVeiculo", "tipo_veiculo", envioArmarazem.idVeiculo);
            ViewBag.statusEnvio = new SelectList(db.StatusEnvioArmazem, "idStatusEnvio", "statusEnvio", envioArmarazem.statusEnvio);
            return View(envioArmarazem);
        }

        // GET: EnvioArmarazem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnvioArmarazem envioArmarazem = db.EnvioArmarazem.Find(id);
            if (envioArmarazem == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDemanda = new SelectList(db.DemandaFinal_produtor, "idDemandaFinal", "idDemandaFinal", envioArmarazem.idDemanda);
            ViewBag.idVeiculo = new SelectList(db.Veiculo, "idVeiculo", "tipo_veiculo", envioArmarazem.idVeiculo);
            ViewBag.statusEnvio = new SelectList(db.StatusEnvioArmazem, "idStatusEnvio", "statusEnvio", envioArmarazem.statusEnvio);
            return View(envioArmarazem);
        }

        // POST: EnvioArmarazem/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEnvio,idDemanda,idVeiculo,statusEnvio,ano_envio,mes_envio,dia_envio")] EnvioArmarazem envioArmarazem)
        {
            if(envioArmarazem.statusEnvio == 2)
            {
                DemandaFinal_produtor demanda = db.DemandaFinal_produtor.Find(envioArmarazem.idDemanda);
                demanda.status_demanda = 2;
                DemandaFinalProdutorController demandaController = new DemandaFinalProdutorController();
                demandaController.Edit(demanda.idDemandaFinal);
            }
            if (ModelState.IsValid)
            {
                db.Entry(envioArmarazem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDemanda = new SelectList(db.DemandaFinal_produtor, "idDemandaFinal", "idDemandaFinal", envioArmarazem.idDemanda);
            ViewBag.idVeiculo = new SelectList(db.Veiculo, "idVeiculo", "tipo_veiculo", envioArmarazem.idVeiculo);
            ViewBag.statusEnvio = new SelectList(db.StatusEnvioArmazem, "idStatusEnvio", "statusEnvio", envioArmarazem.statusEnvio);
            return View(envioArmarazem);
        }

        // GET: EnvioArmarazem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnvioArmarazem envioArmarazem = db.EnvioArmarazem.Find(id);
            if (envioArmarazem == null)
            {
                return HttpNotFound();
            }
            return View(envioArmarazem);
        }

        // POST: EnvioArmarazem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnvioArmarazem envioArmarazem = db.EnvioArmarazem.Find(id);
            db.EnvioArmarazem.Remove(envioArmarazem);
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
