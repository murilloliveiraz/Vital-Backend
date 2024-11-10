using Application.DTOS.Servicos;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Services.Classes
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _servicoRepository;
        private readonly IMapper _mapper;
        public ServicoService(IMapper mapper, IServicoRepository servicoRepository)
        {
            _mapper = mapper;
            _servicoRepository = servicoRepository;
        }
        public async Task<ServicoResponseContract> Create(ServicoRequestContract model)
        {
            Servico servico = _mapper.Map<Servico>(model);
            servico = await _servicoRepository.Create(servico);
            return _mapper.Map<ServicoResponseContract>(servico);
        }
 
        public async Task Delete(int id)
        {
            var servico = await _servicoRepository.GetById(id);
            await _servicoRepository.Delete(_mapper.Map<Servico>(servico));
        }

        public async Task<IEnumerable<ServicoResponseContract>> Get()
        {
            var servicos = await _servicoRepository.Get();
            return servicos.Select(s => _mapper.Map<ServicoResponseContract>(s));
        }

        public async Task<IEnumerable<ServicoResponseContract>> GetAllAppointmentsServices()
        {
            var servicos = await _servicoRepository.GetAllAppointmentsServices();
            return servicos.Select(s => _mapper.Map<ServicoResponseContract>(s));
        }

        public async Task<IEnumerable<ServicoResponseContract>> GetAllExamsServices()
        {
            var servicos = await _servicoRepository.GetAllExamsServices();
            return servicos.Select(s => _mapper.Map<ServicoResponseContract>(s));
        }

        public async Task<IEnumerable<ServicoResponseContract>> GetAllIncludingDeleteds()
        {
            var servicos = await _servicoRepository.GetAllIncludingDeleteds();
            return servicos.Select(s => _mapper.Map<ServicoResponseContract>(s));
        }

        public async Task<ServicoResponseContract> GetById(int id)
        {
            var servico = await _servicoRepository.GetById(id);
            return _mapper.Map<ServicoResponseContract>(servico);
        }

        public async Task<ServicoResponseContract> GetByName(string name)
        {
            var servico = await _servicoRepository.GetByName(name);
            return _mapper.Map<ServicoResponseContract>(servico);
        }

        public async Task<ServicoResponseContract> Update(int id, ServicoRequestContract model)
        {
            var servico = await _servicoRepository.GetById(id);
            _mapper.Map(model, servico);
            await _servicoRepository.Update(servico);
            return _mapper.Map<ServicoResponseContract>(servico);
        }
    }
}