using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using SupplyProject.Models;
using SupplyProject.Services;

namespace SupplyProject.Filtros
{
    [HandleError]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AutorizacaoDeAcesso : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filtroDeContexto)
        {
            var controller = filtroDeContexto.
                ActionDescriptor.
                ControllerDescriptor.
                ControllerName;

            var action = filtroDeContexto.
                ActionDescriptor.
                ActionName;
            
            if (controller != "Home" || action != "Login")
            {
                if (UsuarioService.
                    VerificaSeOUsuarioEstaLogado() == null)
                {
                    filtroDeContexto.
                        RequestContext.
                        HttpContext.
                        Response.
                        Redirect("/Usuarios/Index?Url=" +
                                 filtroDeContexto.HttpContext.Request.Url?.LocalPath);
                }
                //else
                //{
                //    filtroDeContexto.
                //        RequestContext.
                //        HttpContext.
                //        Response.
                //        Redirect("/HomeLogado/Index");
                //}
                
            }
            
        }
       
    }
}