using Application.DTOS.Paciente;
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

        public PacienteService(IUsuarioService usuarioService, IMapper mapper, IPacienteRepository pacienteRepository)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<PacienteResponseContract> Create(PacienteRequestContract model)
        {
            var user = _mapper.Map<UsuarioRequestContract>(model);
            var userCreated = await _usuarioService.Register(user);
            var paciente = _mapper.Map<Paciente>(model);
            paciente.UserId = userCreated.Id;
            await _pacienteRepository.Create(paciente);
            return _mapper.Map<PacienteResponseContract>(paciente);
        }

        public async Task Delete(int id)
        {
            var paciente = await _pacienteRepository.GetById(id);
            await _pacienteRepository.Delete(_mapper.Map<Paciente>(paciente));
        }

        public Task<IEnumerable<PacienteResponseContract>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<PacienteResponseContract?> GetByCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<PacienteResponseContract?> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<PacienteResponseContract> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PacienteResponseContract?> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<PacienteResponseContract> Update(int id, PacienteRequestContract model)
        {
            throw new NotImplementedException();
        }
    }
}