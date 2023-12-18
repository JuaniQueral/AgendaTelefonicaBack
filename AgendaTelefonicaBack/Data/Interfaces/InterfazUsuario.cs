using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaTelefonicaBack.Models;

namespace AgendaTelefonicaBack.Data.Interfaces
{
    public interface InterfazUsuario
    {
        Task<Usuario> AddUser(Usuario register);
        Task DeleteUser(Usuario register);
        Task<List<Usuario>> GetListUsuario();
        Task<Usuario> GetUsuarioById(int id);
        Task UpdateUsuario(Usuario register);

        Usuario? ValidateUser(string username, string password);
    }
}

