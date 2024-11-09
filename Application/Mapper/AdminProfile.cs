using Application.DTOS.Admin;
using Application.DTOS.Usuario;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Administrador, UsuarioRequestContract>().ReverseMap();
            CreateMap<Administrador, UsuarioResponseContract>().ReverseMap();
            CreateMap<Administrador, AdminRequestContract>().ReverseMap();
            CreateMap<Administrador, AdminResponseContract>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Usuario.Id))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Usuario.Role))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.Usuario.CPF))
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.Usuario.DataCriacao))
                .ForMember(dest => dest.DataInativacao, opt => opt.MapFrom(src => src.Usuario.DataInativacao))
                .ForMember(dest => dest.CriadoPorEmail, opt => opt.MapFrom(src => src.Usuario.CriadoPorEmail))
                .ReverseMap();
        }
    }
}
