﻿using System;
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
    public class ProdutosFornecedorController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: ProdutosFornecedor
        public ActionResult Index()
        {
            var produto_fornecedor = db.Produto_fornecedor.Include(p => p.Fornecedor);
            return View(produto_fornecedor.ToList());
        }

        // GET: ProdutosFornecedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto_fornecedor produto_fornecedor = db.Produto_fornecedor.Find(id);
            if (produto_fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(produto_fornecedor);
        }

        // GET: ProdutosFornecedor/Create
        public ActionResult Create()
        {
            ViewBag.Fornecedor_idFornecedor = new SelectList(db.Fornecedor, "idFornecedor", "nome_fornecedor");
            return View();
        }

        // POST: ProdutosFornecedor/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProduto_fornecedor,nome_prodF,preco_prodF,peso_prodF,largura_prodF,altura_prodF,profundidade_prodF,quantidade_prodF,tempo_producaoF,Fornecedor_idFornecedor")] Produto_fornecedor produto_fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Produto_fornecedor.Add(produto_fornecedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fornecedor_idFornecedor = new SelectList(db.Fornecedor, "idFornecedor", "nome_fornecedor", produto_fornecedor.Fornecedor_idFornecedor);
            return View(produto_fornecedor);
        }

        // GET: ProdutosFornecedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto_fornecedor produto_fornecedor = db.Produto_fornecedor.Find(id);
            if (produto_fornecedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fornecedor_idFornecedor = new SelectList(db.Fornecedor, "idFornecedor", "nome_fornecedor", produto_fornecedor.Fornecedor_idFornecedor);
            return View(produto_fornecedor);
        }

        // POST: ProdutosFornecedor/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProduto_fornecedor,nome_prodF,preco_prodF,peso_prodF,largura_prodF,altura_prodF,profundidade_prodF,quantidade_prodF,tempo_producaoF,Fornecedor_idFornecedor")] Produto_fornecedor produto_fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto_fornecedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fornecedor_idFornecedor = new SelectList(db.Fornecedor, "idFornecedor", "nome_fornecedor", produto_fornecedor.Fornecedor_idFornecedor);
            return View(produto_fornecedor);
        }

        // GET: ProdutosFornecedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto_fornecedor produto_fornecedor = db.Produto_fornecedor.Find(id);
            if (produto_fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(produto_fornecedor);
        }

        // POST: ProdutosFornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto_fornecedor produto_fornecedor = db.Produto_fornecedor.Find(id);
            db.Produto_fornecedor.Remove(produto_fornecedor);
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
