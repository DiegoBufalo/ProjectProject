using SupplyProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplyProject.Controllers
{
    public class UsuarioRemoveSessaoController : BaseController
    {
        // GET: UsuarioRemoveSessao
        public ActionResult RemoveSessao()
        {
            Response.Cookies["UserCookieAuthentication"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index","Home");
        }
    }
}