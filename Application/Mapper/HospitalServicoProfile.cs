using Application.DTOS.HospitalServico;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
     public class HospitalServicoProfile: Profile 
    {
        public HospitalServicoProfile()
        {
            CreateMap<HospitalServico, HospitalServicoRequestContract>().ReverseMap();
            CreateMap<HospitalServico, HospitalServicoResponseContract>().ReverseMap();
        }
    }
}