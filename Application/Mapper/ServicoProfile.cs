using Application.DTOS.Servicos;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class ServicoProfile: Profile 
    {
        public ServicoProfile()
        {
            CreateMap<Servico, ServicoRequestContract>().ReverseMap();
            CreateMap<Servico, ServicoResponseContract>().ReverseMap();
        }
    }
}