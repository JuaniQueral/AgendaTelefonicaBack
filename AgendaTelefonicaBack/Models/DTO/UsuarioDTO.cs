using AgendaTelefonicaBack.Models;
using System.ComponentModel.DataAnnotations;

namespace AgendaTelefonicaBack.Models.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string Telefono { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ICollection<ContactoDTO> Contactos { get; set; }

        public UsuarioDTO()
        {
            Contactos = new List<ContactoDTO>();
        }
    }
}
