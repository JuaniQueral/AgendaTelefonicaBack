using AgendaTelefonicaBack.Models;
using AgendaTelefonicaBack.Models.DTO;
using AutoMapper;

namespace AgendaTelefonicaBack.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}
