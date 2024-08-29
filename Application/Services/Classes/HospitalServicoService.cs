using Application.DTOS.HospitalServico;
using Application.DTOS.Servicos;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Services.Classes
{
        public class HospitalServicoService : IHospitalServicoService
    {
        private readonly IHospitalServicoRepository _hospitalServicoRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IMapper _mapper;
        public HospitalServicoService(IMapper mapper, IHospitalServicoRepository hospitalServicoRepository, IServicoRepository servicoRepository)
        {
            _mapper = mapper;
            _hospitalServicoRepository = hospitalServicoRepository;
            _servicoRepository = servicoRepository;
        }

        public async Task<HospitalServicoResponseContract> Create(HospitalServicoRequestContract model)
        {
            HospitalServico hospitalServico = _mapper.Map<HospitalServico>(model);
            hospitalServico = await _hospitalServicoRepository.Create(hospitalServico);
            return _mapper.Map<HospitalServicoResponseContract>(hospitalServico);
        }

        public async Task Delete(int hospitalId, int servicoId)
        {
            var hospitalServico = await _hospitalServicoRepository.GetByHospitalIdAndServicoId(hospitalId, servicoId);

            if (hospitalServico != null)
            {
                await _hospitalServicoRepository.Delete(hospitalServico);
            }
        }

        public async Task<ServicoResponseContract?> GetByHospitalIdAndServicoId(int hospitalId, int servicoId)
        {
            var hospitalServico = await _hospitalServicoRepository.GetByHospitalIdAndServicoId(hospitalId, servicoId);

            return _mapper.Map<ServicoResponseContract>(hospitalServico);
        }

        public async Task<IEnumerable<ServicoResponseContract?>> GetAllByHospitalId(int hospitalId)
        {
            var servicosDoHospital = await _hospitalServicoRepository.GetAllByHospitalId(hospitalId);
            return servicosDoHospital.Select(hs => _mapper.Map<ServicoResponseContract>(hs.Servico));
        }
    }
}