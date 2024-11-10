using Application.DTOS.Consulta;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class ConsultaProfile : Profile
    {
        public ConsultaProfile()
        {
            CreateMap<Consulta, AgendarConsultaRequestContract>().ReverseMap();
            CreateMap<Consulta, AgendarConsultaResponseContract>().ReverseMap();
            CreateMap<Consulta, ConsultaConcluidaResponse>().ReverseMap();
        }
    }
}
