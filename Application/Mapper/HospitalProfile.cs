using Application.DTOS.Hospital;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class HospitalProfile: Profile 
    {
        public HospitalProfile()
        {
            CreateMap<Hospital, HospitalRequestContract>().ReverseMap();
            CreateMap<Hospital, HospitalResponseContract>().ReverseMap();
        }
    }
}