using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaTelefonicaBack.Data.Interfaces;
using AgendaTelefonicaBack.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaTelefonicaBack.Data.Repository
{
    public class UsuarioRepository : InterfazUsuario
    {

        
            private readonly AplicationDbContext _context;

        public UsuarioRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> AddUser(Usuario user)
        {
            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(Usuario user)
        {
            _context.Usuario.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> GetListUsuario()
        {
            return await _context.Usuario.Include(x => x.Contactos).ToListAsync();
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _context.Usuario.Include(x => x.Contactos).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateUsuario(Usuario register)
        {
            var registerItem = await _context.Usuario.FirstOrDefaultAsync(x => x.Id == register.Id);

            if (registerItem != null)
            {
                registerItem.Nombre = register.Nombre;
                registerItem.Apellido = register.Apellido;
                registerItem.UserName = register.UserName;
                registerItem.Telefono = register.Telefono;
                registerItem.Password = register.Password;

                await _context.SaveChangesAsync();
            }
        }

        public Usuario? ValidateUser(string username, string password)
        {
            return _context.Usuario.FirstOrDefault(p => p.UserName == username && p.Password == password);
        }
    }
}



