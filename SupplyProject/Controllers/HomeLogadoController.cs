using SupplyProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SupplyProject.Models;

namespace SupplyProject.Controllers
{
    public class HomeLogadoController : BaseController
    {
        private SupplyProject_dbEntities db = new SupplyProject_dbEntities();

        public ActionResult Index()
        {
            if(UsuarioService.VerificaSeOUsuarioEstaLogado() != null)
            {
                var usuario = HttpContext.Request.Cookies["UserCookieAuthentication"];

                long idUsuario = Convert.ToInt64(CriptografiaService.Descriptografar(usuario.Values["IDUsuario"]));
                var usuarioRetornado = UsuarioService.RecuperaUsuarioPorId(idUsuario);
                Session["tipoUsuario"] = usuarioRetornado.tipo_usuario;
                
                var notificacoes = db.Notificacoes
                    .Where(t => t.idUsuario == idUsuario)
                    .ToList();
             
                Session["notificacoes"] = notificacoes.Count.ToString();


                return View();
            }
            else
            {
                return View("Index","Usuarios");
            }
            

            
        }
        public void RegistrarUsuario()
        {
            

        }
    }

}