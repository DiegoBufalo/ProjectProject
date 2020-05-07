using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupplyProject.Services
{
    public class CookieService
    { 
        public static void RegistraCookieAutenticacao(long IDUsuario)
        {
            //Criando um objeto cookie
            HttpCookie userCookie = new HttpCookie("UserCookieAuthentication");

            //Setando o ID do usuário no cookie
            userCookie.Values["IDUsuario"] = CriptografiaService.Criptografar(IDUsuario.ToString());

            //Definindo o prazo de vida do cookie
            userCookie.Expires = DateTime.Now.AddDays(1);

            //Adicionando o cookie no contexto da aplicação
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }


    }
    }