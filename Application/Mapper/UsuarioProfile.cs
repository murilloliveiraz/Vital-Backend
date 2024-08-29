using Application.DTOS.Usuario;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class UsuarioProfile: Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioRequestContract>().ReverseMap();
            CreateMap<Usuario, UsuarioResponseContract>().ReverseMap();
        }
    }
}
