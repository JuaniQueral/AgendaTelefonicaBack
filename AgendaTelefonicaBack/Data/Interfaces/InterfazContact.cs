using AgendaTelefonicaBack.Models;

namespace AgendaTelefonicaBack.Data.Interfaces
{
    public interface InterfazContact
    {
        Task<List<Contacto>> GetListContacto(int UserId);
        Task<Contacto> GetContacto(int id);
        Task DeleteContacto(Contacto contacto);
        Task<Contacto> AddContacto(Contacto contacto);
        Task UpdateContacto(Contacto contacto);
    }
}
