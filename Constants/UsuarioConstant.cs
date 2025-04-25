using WebAppTienda.Models;

namespace WebAppTienda.Constants
{
    public class UsuarioConstant
    {
        public static List<Usuario> Usuarios = new List<Usuario>()
        {
            new Usuario(){Nombre = "jhonj", Clave = "j123", Celular = "3123123123", Email = "jhonj@gmail.com", Roles = "Administrador" },
            new Usuario(){Nombre = "juanc", Clave = "j123", Celular =" 3113113113", Email = "juanc@gmail.com", Roles = "Cliente" }
        };
    }
}
