using SupplyProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SupplyProject.Controllers
{
    public class HomeLogadoController : BaseController
    {
        public ActionResult Index()
        {
            var usuario = HttpContext.Request.Cookies["UserCookieAuthentication"];

            long idUsuario = Convert.ToInt64(CriptografiaService.Descriptografar(usuario.Values["IDUsuario"]));
            var usuarioRetornado = UsuarioService.RecuperaUsuarioPorId(idUsuario);
            Session["tipoUsuario"] = usuarioRetornado.tipo_usuario;

            return View();
        }
        public void RegistrarUsuario()
        {
            

        }
    }

}