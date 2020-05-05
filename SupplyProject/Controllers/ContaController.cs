using SupplyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplyProject.Controllers
{
    public class ContaController : Controller
    {
        // GET: Conta
        public ActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrar(UsuariosController usuariosController)
        {
            return View();
        }
    }
}