using Application.DTOS.RegistroProntuario;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Humanizer;
using Infraestructure.Repositories.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Application.Services.Classes
{
    public class ProntuarioService : IProntuarioService
    {
        private readonly IProntuarioRepository _prontuarioRepository;
        private readonly IRegistroRepository _registroRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;

        public ProntuarioService(
            IProntuarioRepository prontuarioRepository,
            IRegistroRepository registroRepository,
            IMapper mapper,
            IPacienteRepository pacienteRepository)
        {
            _prontuarioRepository = prontuarioRepository;
            _registroRepository = registroRepository;
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Prontuario> Create(int pacienteId)
        {
            var prontuario = new Prontuario
            {
                PacienteId = pacienteId,
                DataDeCriacao = DateTime.UtcNow,
            };

            prontuario = await _prontuarioRepository.Create(prontuario);
            var conteudo = new RegistroRequestContract
            {
                Tipo = "Criação",
                Conteudo = new
                {
                   tipo = "Criação",
                   observacao = "Prontuário criado automaticamente ao registrar o paciente." 
                }
            };

            await CreateRecord(prontuario.Id, conteudo);
            return prontuario;
        }

        public async Task CreateRecord(int prontuarioId, RegistroRequestContract conteudo)
        {
            var registro = new ProntuarioRegistro
            {
                ProntuarioId = prontuarioId,
                Tipo = conteudo.Tipo,
                Data = DateTime.Now,
                Conteudo = (conteudo.Conteudo as JObject)?.ToObject<Dictionary<string, object>>().ToBsonDocument()
                  ?? conteudo.Conteudo.ToBsonDocument()
            };

            await _registroRepository.CreateRecord(registro);
        }

        public async Task<ICollection<RegistroResponseContract>> GetAllRecords(int pacienteId)
        {
            var paciente = await _pacienteRepository.GetById(pacienteId);

            if (paciente?.Prontuario == null)
                return new List<RegistroResponseContract>();

            var registros = await _registroRepository.GetAllRecords(paciente.Prontuario.Id);

            return registros.Select(r => new RegistroResponseContract
            {
                Id = r.Id.ToString(),
                ProntuarioId = r.ProntuarioId,
                Tipo = r.Tipo,
                Data = r.Data,
                Conteudo = BsonTypeMapper.MapToDotNetValue(r.Conteudo)
            }).ToList();
        }

        public async Task<RegistroResponseContract> GetById(ObjectId registroId)
        {
            var registro = await _registroRepository.GetById(registroId);
            return new RegistroResponseContract
            {
                Id = registro.Id.ToString(),
                ProntuarioId = registro.ProntuarioId,
                Tipo = registro.Tipo,
                Data = registro.Data,
                Conteudo = BsonTypeMapper.MapToDotNetValue(registro.Conteudo)
            };
        }
    }
}
