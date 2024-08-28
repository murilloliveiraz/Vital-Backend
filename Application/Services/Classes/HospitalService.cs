using Application.DTOS.Hospital;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Services.Classes
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IMapper _mapper;
        public HospitalService(IMapper mapper, IHospitalRepository hospitalRepository)
        {
            _mapper = mapper;
            _hospitalRepository = hospitalRepository;
        }

        public async Task<HospitalResponseContract> Create(HospitalRequestContract model)
        {
            Hospital hospital = _mapper.Map<Hospital>(model);
            hospital = await _hospitalRepository.Create(hospital);
            return _mapper.Map<HospitalResponseContract>(hospital);
        }

        public async Task Delete(int id)
        {
            var hospital = await _hospitalRepository.GetById(id);
            await _hospitalRepository.Delete(_mapper.Map<Hospital>(hospital));
        }

        public async Task<IEnumerable<HospitalResponseContract>> Get()
        {
            var hospitais = await _hospitalRepository.Get();
            return hospitais.Select(h => _mapper.Map<HospitalResponseContract>(h));
        }

        public async Task<IEnumerable<HospitalResponseContract>?> GetAllByLocation(string estado)
        {
            var hospitais = await _hospitalRepository.GetAllByLocation(estado);
            return hospitais.Select(h => _mapper.Map<HospitalResponseContract>(h));
        }

        public async Task<HospitalResponseContract> GetById(int id)
        {
            var hospital = await _hospitalRepository.GetById(id);
            return _mapper.Map<HospitalResponseContract>(hospital);
        }

        public async Task<HospitalResponseContract> GetByName(string name)
        {
            var hospital = await _hospitalRepository.GetByName(name);
            return _mapper.Map<HospitalResponseContract>(hospital);
        }

        public async Task<HospitalResponseContract> Update(int id, HospitalRequestContract model)
        {
            var hospital = await _hospitalRepository.GetById(id);
            _mapper.Map(model, hospital);
            await _hospitalRepository.Update(hospital);
            return _mapper.Map<HospitalResponseContract>(hospital);
        }
    }
}