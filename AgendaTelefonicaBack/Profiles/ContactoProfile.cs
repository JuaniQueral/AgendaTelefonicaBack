using AutoMapper;
using AgendaTelefonicaBack.Models.DTO;
using AgendaTelefonicaBack.Models;

namespace AgendaTelefonicaBack.Models.Profiles
{
    public class ContactoProfile : Profile
    {
        public ContactoProfile()
        {
            CreateMap<Contacto, ContactoDTO>();
            CreateMap<ContactoDTO, Contacto>();
        }
    }
}
