using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Remotion.Linq.Parsing.ExpressionVisitors.TreeEvaluation;
using SupplyProject.Models;
using SupplyProject.Services;

namespace SupplyProject.Controllers
{
    public class PedidoFinalUsuarioController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        // GET: PedidoFinalUsuario
        public ActionResult Index()
        {
            var pedidoFinal_usuario = db.PedidoFinal_usuario.Include(p => p.Armazem).Include(p => p.Produto_fornecedor).Include(p => p.StatusPedido1).Where(p => p.statusPedido == 1).Include(p => p.Usuario).Include(p => p.EnvioFornecedor);

            return View(pedidoFinal_usuario.ToList());
        }

        public ActionResult ConfirmarPedido()
        {
            return View();
        }

        public ActionResult IndexEncerrado()
        {
            var pedidoFinal_usuario = db.PedidoFinal_usuario.Include(p => p.Armazem).Include(p => p.Produto_fornecedor).Include(p => p.StatusPedido1).Where(p => p.statusPedido == 2).Include(p => p.Usuario);
            return View(pedidoFinal_usuario.ToList());
        }
        public ActionResult ExibirEstatisticas()
        {
            return View();
        }

        public ActionResult ExibirPedidoPorRegiao()
        {
            return View();
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

            //dados fornecedor
            Produto_fornecedor prodForn= db.Produto_fornecedor.Find(pedidoFinal_usuario.Produto_fornecedor_idProduto_fornecedor);
            Fornecedor fornecedor = db.Fornecedor.Find(prodForn.Fornecedor_idFornecedor);
            String cepForn = fornecedor.CEP;

            //dados armazem
            Produto_armazem prodArm = db.Produto_armazem.Find(prodForn.idProduto_fornecedor);
            Armazem armazem = db.Armazem.Find(prodArm.Armazem_idArmazem);
            String cepArmazem = armazem.CEP;

            //carrega o xml
            CalculaFreteController calculaFrete = new CalculaFreteController();
            XElement xml = calculaFrete.CalculaDistancia(cepForn, cepArmazem);
            // Formatar a resposta
            /*
            String valores = string.Format("Origem: {0} \n Destino: {1} \n Duração da Viagem: {2} \n Distância: {3}",
                //Pegar endereço de origem
                xml.Element("origin_address").Value,
                //Pegar endereço de destino
                xml.Element("destination_address").Value,
                //Pegar duração
                xml.Element("row").Element("element").Element("duration").Element("text").Value,
                //Pegar distância ente os dois pontos
                xml.Element("row").Element("element").Element("distance").Element("text").Value
                );*/
            String origem = string.Format("Origem: {0}", xml.Element("origin_address").Value);
            String destino = string.Format("Destino: {0}", xml.Element("destination_address").Value);
            String distancia = string.Format("Distancia: {0}", xml.Element("row").Element("element").Element("distance").Element("text").Value);
            String duracao = string.Format("Duração: {0}", xml.Element("row").Element("element").Element("duration").Element("text").Value);
            String valores = origem + "\n" + destino + "\n" + distancia + "\n" + duracao;

            ViewBag.DadosFrete = valores;

            return View(pedidoFinal_usuario);
        }

        public ActionResult DetailsEncerrado(int? id)
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

            EnvioFornecedor envioFornecedor = db.EnvioFornecedor.Where(t => t.idPedido == pedidoFinal_usuario.idPedido).FirstOrDefault();

            //dados fornecedor
            Produto_fornecedor prodForn = db.Produto_fornecedor.Find(pedidoFinal_usuario.Produto_fornecedor_idProduto_fornecedor);
            Fornecedor fornecedor = db.Fornecedor.Find(prodForn.Fornecedor_idFornecedor);
            String cepForn = fornecedor.CEP;

            //valor de custo
            Veiculo veiculo = db.Veiculo.Find(envioFornecedor.idVeiculo);
            double.TryParse(veiculo.custo_frete, out double custoPKM);

            //dados armazem
            Produto_armazem prodArm = db.Produto_armazem.Find(prodForn.idProduto_fornecedor);
            Armazem armazem = db.Armazem.Find(prodArm.Armazem_idArmazem);
            String cepArmazem = armazem.CEP;

            //carrega o xml
            CalculaFreteController calculaFrete = new CalculaFreteController();
            XElement xml = calculaFrete.CalculaDistancia(cepForn, cepArmazem);

            String origem = string.Format("Origem: {0}", xml.Element("origin_address").Value);
            String destino = string.Format("Destino: {0}", xml.Element("destination_address").Value);
            String distancia = string.Format("Distancia: {0}", xml.Element("row").Element("element").Element("distance").Element("text").Value);
            String duracao = string.Format("Duração: {0}", xml.Element("row").Element("element").Element("duration").Element("text").Value);


            var element = xml.Element("row").Element("element").Element("distance").Element("text").Value;
            var result = System.Text.RegularExpressions.Regex.Split(element," ");

            var distance = result[0];
            double distancaeInt = Convert.ToDouble(distance);

            double Custo = distancaeInt * custoPKM;
            String custoTotal = "Frete: " + Custo.ToString();

            String valores = origem + "\n" + destino + "\n" + distancia + "\n" + duracao + "\n" + custoTotal;

            ViewBag.DadosFrete = valores;

            return View(pedidoFinal_usuario);
        }

        // GET: PedidoFinalUsuario/Create
        public ActionResult Create()
        {
            int id = UsuarioService.VerificaSeOUsuarioEstaLogado().idUsuario;

            ViewBag.Armazem_idArmazem = new SelectList(db.Armazem, "idArmazem", "nome_armazem");
            ViewBag.Produto_fornecedor_idProduto_fornecedor = new SelectList(db.Produto_fornecedor, "idProduto_fornecedor", "nome_prodF");
            ViewBag.statusPedido = new SelectList(db.StatusPedido, "idStatus", "nome_status");
            ViewBag.Usuario_idUsuario = new SelectList(db.Usuario.Where(e => e.idUsuario == id), "idUsuario", "nome_usuario");
            return View();
        }

        // POST: PedidoFinalUsuario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPedido,Usuario_idUsuario,Produto_fornecedor_idProduto_fornecedor,ano_pedido,mes_pedido,dia_pedido,quantidade")] PedidoFinal_usuario pedidoFinal_usuario)
        {
            int produtoForn = pedidoFinal_usuario.Produto_fornecedor_idProduto_fornecedor;
            Produto_armazem produtoArm = db.Produto_armazem.Find(produtoForn);
            int armazemFinal = produtoArm.Armazem_idArmazem;

            Produto_fornecedor produtoFornecedor = db.Produto_fornecedor.Find(pedidoFinal_usuario.Produto_fornecedor_idProduto_fornecedor);
            double precoProduto = produtoFornecedor.preco_prodF;
            pedidoFinal_usuario.preco_pedido = precoProduto * pedidoFinal_usuario. quantidade;
            pedidoFinal_usuario.Armazem_idArmazem = armazemFinal;
            pedidoFinal_usuario.statusPedido = 1;


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

        // GET: PedidoFinalUsuario/VizualizarNFe/5
        public ActionResult VizualizarNFe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoFinal_usuario pedido = db.PedidoFinal_usuario.Find(id);
            if(pedido == null)
            {
                return HttpNotFound();
            }

             Produto_fornecedor produto = db.Produto_fornecedor.Find(pedido.Produto_fornecedor_idProduto_fornecedor);
             int fornecedorResp = produto.Fornecedor_idFornecedor;
             Fornecedor fornecedor = db.Fornecedor.Find(fornecedorResp);

            //VAI SER NA GAMBIARRRA-------------------------------------------
            ViewBag.Cnpj = fornecedor.cnpj_fornecedor;
            ViewBag.Nome = fornecedor.nome_fornecedor;
            ViewBag.Cep = fornecedor.CEP;
            ViewBag.Endereco = fornecedor.logradouro_fornecedor;
            ViewBag.Num = fornecedor.numlogradouro_fornecedor;
            ViewBag.Municipio = fornecedor.Municipio;
            ViewBag.UF = fornecedor.UF;
            //---------------------------------------------------------------
            //Pedido-----------------------------------------------------------
            Produto_fornecedor prodforn = db.Produto_fornecedor.Find(pedido.Produto_fornecedor_idProduto_fornecedor);

            ViewBag.idPedido = pedido.idPedido;
            ViewBag.DataHora = pedido.dia_pedido + "/" + pedido.mes_pedido + "/" + pedido.ano_pedido +" - "+ DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            ViewBag.Quantidade = pedido.quantidade;
            ViewBag.Preco = pedido.preco_pedido;
            ViewBag.Produto = prodforn.nome_prodF;
            ViewBag.idProduto = prodforn.idProduto_fornecedor;
            ViewBag.valorUni = prodforn.preco_prodF;
            //Usuario----------------------------------------------------------------
            Usuario usuario = db.Usuario.Find(pedido.Usuario_idUsuario);
            ViewBag.Email = usuario.email_usuario;
            //Armazem--------------------------------------------------------------
            Armazem armazem = db.Armazem.Find(pedido.Armazem_idArmazem);
            ViewBag.EnderecoArmazem = armazem.logradouro_armazem;
            ViewBag.NumeroEnd = armazem.numlogradouro_armazem;
            ViewBag.Nome = armazem.nome_armazem;
            ViewBag.ArmazemCep = armazem.CEP;
            //IMPOSTOS-------------------------------------------------------------
            ViewBag.Aliquota = pedido.preco_pedido * 0.06;
            ViewBag.CSLL = pedido.preco_pedido * 0.035;
            ViewBag.Cofins = pedido.preco_pedido * 0.1151;
            ViewBag.PIS = pedido.preco_pedido * 0.0251;
            ViewBag.IPI = pedido.preco_pedido * 0.075;
            ViewBag.ICMS = pedido.preco_pedido * 0.32;

            ViewBag.Deducao = ViewBag.Aliquota + ViewBag.CSLL + ViewBag.Cofins + ViewBag.PIS + ViewBag.IPI + ViewBag.ICMS;






            return View();
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
        public ActionResult Edit(PedidoFinal_usuario pedidoFinal_usuario)
        {
            //[Bind(Include = "idPedido,Usuario_idUsuario,Produto_fornecedor_idProduto_fornecedor,Armazem_idArmazem,statusPedido,preco_pedido,ano_pedido,mes_pedido,dia_pedido,quantidade")]
            if (ModelState.IsValid)
            {
                db.Entry(pedidoFinal_usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int qtdPedido = pedidoFinal_usuario.quantidade;
            Produto_armazem produtoArmazem = db.Produto_armazem.Find(pedidoFinal_usuario.Produto_fornecedor_idProduto_fornecedor);
            int qtdEstoque = produtoArmazem.quantidade_prodA;
            produtoArmazem.quantidade_prodA = qtdEstoque + qtdPedido;

            ProdutosArmazemController prod = new ProdutosArmazemController();
            prod.AtulizaArmazem(produtoArmazem);

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

        public ActionResult SalvarAvaliacao(AvaliacaoDto avaliacaoDto)
        {
            var result = 1;

            try
            {
                int.TryParse(avaliacaoDto.Id, out int idAvalicao);
                int.TryParse(avaliacaoDto.Nota, out int notaResult);
                int.TryParse(avaliacaoDto.Id, out int idResult);

                var avaliacao = new Avaliacao {idAvaliacao = idAvalicao, nota = notaResult, texto = avaliacaoDto.Texto};

                var pedido = db.PedidoFinal_usuario.Find(idResult);
                pedido.Avaliar = 1;

                db.Avaliacao.Add(avaliacao);

                db.SaveChanges();
            }
            catch (Exception e)
            {
                result = 0;
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
