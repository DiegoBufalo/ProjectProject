using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplyProject.Models;
using SupplyProject.Services;

namespace SupplyProject.Controllers
{
    public class UsuarioAutenticacaoController : Controller
    {
        // GET: UsuarioAutenticacao
        [HttpGet]
        public JsonResult AutenticacaoDeUsuario(string Login, string Senha)
        {
            if (UsuarioService.AutenticarUsuario(Login, Senha))
            {
                return Json(new {
                        OK = true,
                        Mensagem = "Usuário autenticado. Redirecionando..." },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {
                        OK = false,
                        Mensagem = "Usuário não encontrando. Tente novamente." },
                    JsonRequestBehavior.AllowGet);
            }
        }
    }
}