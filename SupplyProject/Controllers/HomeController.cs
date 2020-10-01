using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplyProject.Models;
using SupplyProject.Services;

namespace SupplyProject.Controllers
{
    public class HomeController : Controller
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        public ActionResult Index()
        {
            //var idUsuario =
            int.TryParse(Session["notificacoes"].ToString(), out int idUsuario);

            var notificacoes = db.Notificacoes
                .Where(t => t.idUsuario == idUsuario)
                .ToList();
             
            Session["notificacoes"] = notificacoes.Count.ToString();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult BuscarNotificacoes(int idUsuario)
        {
            var notificacoes = db.Notificacoes.ToList();
            
               /* .Where(t => t.idUsuario == idUsuario)*/

            Session["notificacoes"] = notificacoes.Count.ToString();

            return Json(notificacoes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApagarNotificacoes(int idUsuario)
        {
            var usuario = UsuarioService.VerificaSeOUsuarioEstaLogado() ;

            var notificacoeApagar = db.Notificacoes.Where(n => n.idUsuario == idUsuario);

            db.Notificacoes.RemoveRange(notificacoeApagar);
            
            db.SaveChanges();

            var notificacoes = db.Notificacoes
                .Where(t => t.idUsuario == usuario.idUsuario)
                .ToList();

            Session["notificacoes"] = notificacoes.Count.ToString();

            return Json(notificacoes, JsonRequestBehavior.AllowGet);
        }
    }
}