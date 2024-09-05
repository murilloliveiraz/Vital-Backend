using Application.DTOS.Prontuarios;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class ProntuarioProfile : Profile
    {
        public ProntuarioProfile()
        {
            CreateMap<Prontuario, ProntuarioResponseContract>().ReverseMap();
        }
    }
}
