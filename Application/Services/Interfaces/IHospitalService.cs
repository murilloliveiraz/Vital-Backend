using Application.DTOS.Hospital;

namespace Application.Services.Interfaces
{
    public interface IHospitalService : IService<HospitalRequestContract, HospitalResponseContract, int>
    {
        Task<HospitalResponseContract> GetByName(string name);
        Task<IEnumerable<HospitalResponseContract>?> GetAllByLocation(string estado);
    }

}