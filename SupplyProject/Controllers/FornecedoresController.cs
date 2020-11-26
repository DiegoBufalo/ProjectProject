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
    public class FornecedoresController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: Fornecedores
        public ActionResult Index()
        {

            var avaliacao = new List<NotasDto>();
            avaliacao = db.Avaliacao
               .GroupBy(p => p.refFornecedor)
               .Select(g => new NotasDto
               {
                   Average = g.Average(e => e.nota)
               }).ToList();

            List<double> notas1 = new List<double>();

            foreach (var notas in avaliacao)
            {

                notas1.Add(notas.Average);
            }



            ViewBag.Notas = notas1;
            var fornecedor = db.Fornecedor.ToList();
            return View(fornecedor);
        }

        public ActionResult Avaliacoes(int? id)
        {
            if(id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedor.Find(id);
            if(fornecedor == null)
            {
                return HttpNotFound();
            }

            var avaliacao = db.Avaliacao.ToList().Where(e => e.refFornecedor == id);

            return View(avaliacao);
        }

        // GET: Fornecedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedor.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idFornecedor,nome_fornecedor,cnpj_fornecedor,logradouro_fornecedor,numlogradouro_fornecedor,telefone_fornecedor,email_fornecedor,CEP,Municipio,UF,pais")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Fornecedor.Add(fornecedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fornecedor);
        }

        // GET: Fornecedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedor.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedores/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFornecedor,nome_fornecedor,cnpj_fornecedor,logradouro_fornecedor,numlogradouro_fornecedor,telefone_fornecedor,email_fornecedor,CEP,Municipio,UF,pais")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fornecedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedor.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fornecedor fornecedor = db.Fornecedor.Find(id);
            db.Fornecedor.Remove(fornecedor);
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
