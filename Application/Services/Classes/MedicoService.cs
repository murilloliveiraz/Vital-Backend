using Application.DTOS.Medico;
using Application.DTOS.Usuario;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Services.Classes
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public MedicoService(IMedicoRepository medicoRepository, IMapper mapper, IUsuarioService usuarioService)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        public async Task<MedicoResponseContract> Create(MedicoRequestContract model)
        {
            var user = _mapper.Map<UsuarioRequestContract>(model);
            user.Role = "Medico";
            var userCreated = await _usuarioService.Register(user);
            var medico = _mapper.Map<Medico>(model);
            medico.UserId = userCreated.Id;
            await _medicoRepository.Create(medico);
            return _mapper.Map<MedicoResponseContract>(medico);
        }

        public async Task Delete(int id)
        {
            var medico = await _medicoRepository.GetById(id);
            await _medicoRepository.Delete(_mapper.Map<Medico>(medico));
        }

        public async Task<IEnumerable<MedicoResponseContract>> Get()
        {
            var medicos = await _medicoRepository.Get();
            return medicos.Select(m => _mapper.Map<MedicoResponseContract>(m));
        }

        public async Task<IEnumerable<MedicoResponseContract>?> GetAllByHospitalId(int id)
        {
            var medicos = await _medicoRepository.GetAllByHospitalId(id);
            return medicos.Select(m => _mapper.Map<MedicoResponseContract>(m));
        }

        public async Task<IEnumerable<MedicoResponseContract>?> GetAllBySpecialization(string especialization)
        {
            var medicos = await _medicoRepository.GetAllBySpecialization(especialization);
            return medicos.Select(m => _mapper.Map<MedicoResponseContract>(m));
        }

        public async Task<IEnumerable<MedicoResponseContract>?> GetAllBySpecializationAndHospitalId(string especialization, int id)
        {
            var medicos = await _medicoRepository.GetAllBySpecializationAndHospitalId(especialization, id);
            return medicos.Select(m => _mapper.Map<MedicoResponseContract>(m));
        }

        public async Task<MedicoResponseContract?> GetByCRM(string crm)
        {
            var medico = await _medicoRepository.GetByCRM(crm);
            return _mapper.Map<MedicoResponseContract>(medico);
        }

        public async Task<MedicoResponseContract?> GetByEmail(string email)
        {
            var paciente = await _medicoRepository.GetByEmail(email);
            return _mapper.Map<MedicoResponseContract>(paciente);
        }

        public async Task<MedicoResponseContract> GetById(int id)
        {
            var medico = await _medicoRepository.GetById(id);
            return _mapper.Map<MedicoResponseContract>(medico);
        }

        public async Task<MedicoResponseContract> Update(int id, MedicoRequestContract model)
        {
            var medico = await _medicoRepository.GetById(id);
            _mapper.Map(model, medico);
            await _medicoRepository.Update(medico);
            return _mapper.Map<MedicoResponseContract>(medico);
        }
    }
}