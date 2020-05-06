using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using SupplyProject.Models;

namespace SupplyProject.Services
{
    public class UsuarioService
    {
        public static Usuario RecuperaUsuarioPorId(long IDUsuario)
        {
            try
            {
                using (var db = new SupplyProject_dbEntities())
                {
                    var usuario =
                        db.Usuario.SingleOrDefault(u => u.idUsuario== IDUsuario);
                    return usuario;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Usuario VerificaSeOUsuarioEstaLogado()
        {
            var usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];

            if (usuario == null)
            {
                return null;
            }

            long idUsuario = Convert.ToInt64(CriptografiaService.Descriptografar(usuario.Values["IDUsuario"]));
            var usuarioRetornado = RecuperaUsuarioPorId(idUsuario);
            return usuarioRetornado;
        }
        public static bool AutenticarUsuario(string Login, string Senha)
        {
            var senhaCriptografada = 
                FormsAuthentication.HashPasswordForStoringInConfigFile(Senha, "sha1");

            try
            {
                using (var db = new SupplyProject_dbEntities())
                {
                    var queryAutenticaUsuarios = db.Usuario.SingleOrDefault(x => x.email_usuario == Login && x.senha_usuario == Senha);

                    if (queryAutenticaUsuarios == null)
                    {
                        return false;
                    }

                    CookieService.RegistraCookieAutenticacao(queryAutenticaUsuarios.idUsuario);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}