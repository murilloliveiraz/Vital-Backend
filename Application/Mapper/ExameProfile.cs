using Application.DTOS.Exame;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class ExameProfile : Profile
    {
        public ExameProfile()
        {
            CreateMap<Exame, AdicionarResultadoRequestContract>().ReverseMap();
            CreateMap<Exame, AdicionarResultadoResponseContract>().ReverseMap();
            CreateMap<Exame, AgendarExameRequestContract>().ReverseMap();
            CreateMap<Exame, AgendarExameResponseContract>().ReverseMap();
            CreateMap<Exame, ExameConcluidoResponse>().ReverseMap();
        }
    }
}
