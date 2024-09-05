using Application.DTOS.Paciente;
using Application.DTOS.Servicos;
using Application.DTOS.Usuario;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Services.Classes
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly IProntuarioService _prontuarioService;

        public PacienteService(
            IUsuarioService usuarioService,
            IMapper mapper,
            IPacienteRepository pacienteRepository,
            IProntuarioService prontuarioService
            )
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
            _prontuarioService = prontuarioService;
        }

        public async Task<PacienteResponseContract> Create(PacienteRequestContract model)
        {
            // model.DataNascimento = DateTime.SpecifyKind(model.DataNascimento, DateTimeKind.Unspecified);
            var user = _mapper.Map<UsuarioRequestContract>(model);
            user.Role = "Paciente";
            var userCreated = await _usuarioService.Register(user);
            var paciente = _mapper.Map<Paciente>(model);
            paciente.UserId = userCreated.Id;
            await _pacienteRepository.Create(paciente);
            await _prontuarioService.Create(paciente.Id);
            return _mapper.Map<PacienteResponseContract>(paciente);
        }

        public async Task Delete(int id)
        {
            var paciente = await _pacienteRepository.GetById(id);
            await _pacienteRepository.Delete(_mapper.Map<Paciente>(paciente));
        }

        public async Task<IEnumerable<PacienteResponseContract>> Get()
        {
            var pacientes = await _pacienteRepository.Get();
            return pacientes.Select(p => _mapper.Map<PacienteResponseContract>(p));
        }

        public async Task<PacienteResponseContract?> GetByCPF(string cpf)
        {
            var paciente = await _pacienteRepository.GetByCPF(cpf);
            return _mapper.Map<PacienteResponseContract>(paciente);
        }

        public async Task<PacienteResponseContract?> GetByEmail(string email)
        {
            var paciente = await _pacienteRepository.GetByEmail(email);
            return _mapper.Map<PacienteResponseContract>(paciente);
        }

        public async Task<PacienteResponseContract> GetById(int id)
        {
            var paciente = await _pacienteRepository.GetById(id);
            return _mapper.Map<PacienteResponseContract>(paciente);
        }

        public async Task<PacienteResponseContract?> GetByName(string name)
        {
            var paciente = await _pacienteRepository.GetByName(name);
            return _mapper.Map<PacienteResponseContract>(paciente);
        }

        public async Task<PacienteResponseContract> Update(int id, PacienteRequestContract model)
        {
            var paciente = await _pacienteRepository.GetById(id);
            _mapper.Map(model, paciente);
            await _pacienteRepository.Update(paciente);
            return _mapper.Map<PacienteResponseContract>(paciente);
        }
    }
}