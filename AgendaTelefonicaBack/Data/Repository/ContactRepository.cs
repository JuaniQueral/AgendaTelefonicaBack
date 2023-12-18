using AgendaTelefonicaBack.Models;
using Microsoft.EntityFrameworkCore;
using AgendaTelefonicaBack.Data.Interfaces;

namespace AgendaTelefonicaBack.Data.Repository
{
    public class ContactRepository : InterfazContact 
    {
        private readonly AplicationDbContext _context;

        public ContactRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contacto> AddContacto(Contacto contacto)
        {
            _context.Add(contacto);
            await _context.SaveChangesAsync();
            return contacto;
        }

        public async Task DeleteContacto(Contacto contacto)
        {
            _context.Contacto.Remove(contacto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Contacto>> GetListContacto(int UserId)
        {
            return await _context.Contacto.Where(x => x.UsuarioId == UserId).ToListAsync();
        }

        public async Task<Contacto> GetContacto(int id)
        {
            return await _context.Contacto.FindAsync(id);
        }

        public async Task UpdateContacto(Contacto contacto)
        {
            var contactoItem = await _context.Contacto.FirstOrDefaultAsync(x => x.Id == contacto.Id);

            if (contactoItem != null)
            {
                contactoItem.Nombre = contacto.Nombre;
                contactoItem.Apellido = contacto.Apellido;
                contactoItem.Email = contacto.Email;
                contactoItem.Telefono = contacto.Telefono;
                contactoItem.LlamadasEntrante = contacto.LlamadasEntrante;
                contactoItem.LlamadasSaliente = contacto.LlamadasSaliente;


                await _context.SaveChangesAsync();
            }

        }
    }
}
