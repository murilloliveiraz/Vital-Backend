using Application.DTOS.Consulta;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Google.Apis.Calendar.v3.Data;
using Infraestructure.Services.Interfaces;
using Infraestructure.Repositories.Classes;

namespace Application.Services.Classes
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IPacienteService _pacienteService;
        private readonly IMedicoService _medicoService;
        private readonly IDocumentoService _documentoService;
        private readonly IGoogleMeetService _googleMeetService;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;
        private readonly IEmailService _emailSender;
        private readonly IS3StorageService _s3StorageService;

        public ConsultaService(IMapper mapper, IConsultaRepository consultaRepository, IPacienteService pacienteService, IConfiguration configuration, IEmailService emailSender, IDocumentoService documentoService, IMedicoService medicoService, IGoogleMeetService googleMeetService, IS3StorageService s3StorageService)
        {
            _mapper = mapper;
            _consultaRepository = consultaRepository;
            _pacienteService = pacienteService;
            _configuration = configuration;
            _emailSender = emailSender;
            _documentoService = documentoService;
            _medicoService = medicoService;
            _googleMeetService = googleMeetService;
            _s3StorageService = s3StorageService;
        }

        public async Task<AgendarConsultaResponseContract> Create(AgendarConsultaRequestContract model)
        {
            var paciente = await _pacienteService.GetById(model.PacienteId);
            if (paciente == null)
            {
                throw new Exception("Paciente n�o encontrado.");
            }
            Consulta consulta = _mapper.Map<Consulta>(model);
            consulta.TipoConsulta = "Presencial";
            string dataFormatada = consulta.Data.ToString("yyyyMMdd");
            consulta.PrefixoDaPasta = $"{paciente.CPF}/{dataFormatada}";

            consulta = await _consultaRepository.Create(consulta);
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = consulta.EmailParaReceberNotificacoes,
                Subject = "Confirma��o de agendamento de consulta",
                Body = CommunicationEmail.AppointmentConfirmationEmail(consulta.Nome)
            };
            await _emailSender.SendEmailAsync(mailRequest);
            return _mapper.Map<AgendarConsultaResponseContract>(consulta);
        }

        public async Task<AgendarConsultaResponseContract> CreateRemoteAppointment(AgendarConsultaRequestContract model)
        {
            var paciente = await _pacienteService.GetById(model.PacienteId);
            var medico = await _medicoService.GetById(model.MedicoId);
            if (paciente == null || medico == null)
            {
                throw new Exception("Paciente ou medico não encontrado.");
            }
            Consulta consulta = _mapper.Map<Consulta>(model);
            consulta.TipoConsulta = "Remota";
            string dataFormatada = consulta.Data.ToString("yyyyMMdd");
            consulta.PrefixoDaPasta = $"{paciente.CPF}/{dataFormatada}";

            Event consultaRemota = await _googleMeetService.CreateEvent(consulta, paciente.Email, medico.Email);
            consulta.MeetLink = consultaRemota.HangoutLink;
            consulta = await _consultaRepository.Create(consulta);
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = consulta.EmailParaReceberNotificacoes,
                Subject = "Confirma��o de agendamento de consulta",
                Body = CommunicationEmail.AppointmentConfirmationEmail(consulta.Nome)
            };
            await _emailSender.SendEmailAsync(mailRequest);
            return _mapper.Map<AgendarConsultaResponseContract>(consulta);
        }

        public async Task<ConsultaConcluidaResponse> SetAppointmentAsCompleted(int id)
        {
            var exame = await _consultaRepository.SetAppointmentAsCompleted(id);
            return _mapper.Map<ConsultaConcluidaResponse>(exame);
        }

        public async Task Delete(int id)
        {
            var consulta = await _consultaRepository.GetById(id);
            await _consultaRepository.Delete(_mapper.Map<Consulta>(consulta));
        }

        public async Task<IEnumerable<AgendarConsultaResponseContract>> Get()
        {
            var consultas = await _consultaRepository.Get();
            return consultas.Select(e => _mapper.Map<AgendarConsultaResponseContract>(e));
        }

        public async Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllCompleted()
        {
            var consultas = await _consultaRepository.GetAllCompleted();
            return consultas.Select(e => _mapper.Map<ConsultaConcluidaResponse>(e));
        }

        public async Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllDoctorAppointmentsCompleted(int id)
        {
            var consultas = await _consultaRepository.GetAllDoctorAppointmentsCompleted(id);
            return consultas.Select(e => _mapper.Map<ConsultaConcluidaResponse>(e));
        }

        public async Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllDoctorAppointmentsScheduled(int id)
        {
            var consultas = await _consultaRepository.GetAllDoctorAppointmentsScheduled(id);
            return consultas.Select(e => _mapper.Map<AgendarConsultaResponseContract>(e));
        }

        public async Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllPatientAppointmentsCompleted(int id)
        {
            var consultas = await _consultaRepository.GetAllPatientAppointmentsCompleted(id);
            var consultasResponse = consultas.Select(e => _mapper.Map<ConsultaConcluidaResponse>(e)).ToList();
            var bucketname = _configuration["S3Storage:Bucket-Name"];

            var tasks = consultasResponse.SelectMany(consulta =>
                consulta.Documentos.Select(async documento =>
                {
                    var doc = _mapper.Map<AdicionarDocumentoResponseContract>(documento);
                    var file = await _s3StorageService.GetFileByKeyAsync(doc.S3KeyPath, bucketname);
                    if (file.Success)
                    {
                        doc.ArquivoResultadoUrl = file.PresignedUrl;
                    }
                })
            );

            await Task.WhenAll(tasks);
            return consultasResponse;
        }

        public async Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllPatientAppointmentsScheduled(int id)
        {
            var consultas = await _consultaRepository.GetAllPatientAppointmentsScheduled(id);
            return consultas.Select(e => _mapper.Map<AgendarConsultaResponseContract>(e));
        }

        public async Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllScheduled()
        {
            var consultas = await _consultaRepository.GetAllScheduled();
            var responses = new List<AgendarConsultaResponseContract>();

            foreach (var exame in consultas.Take(10))
            {
                var paciente = await _pacienteService.GetById(exame.PacienteId);
                var response = _mapper.Map<AgendarConsultaResponseContract>(exame);
                response.PacienteNome = paciente?.Nome;
                response.PacienteCPF = paciente?.CPF;
                responses.Add(response);
            }

            return responses;
        }

        public async Task<AgendarConsultaResponseContract> UpdatePaymentStatus(int id)
        {
            var consulta = await _consultaRepository.UpdatePaymentStatus(id);
            return _mapper.Map<AgendarConsultaResponseContract>(consulta);
        }

        public async Task<AgendarConsultaResponseContract> GetById(int id)
        {
            var consulta = await _consultaRepository.GetById(id);
            return _mapper.Map<AgendarConsultaResponseContract>(consulta);
        }

        public async Task<AgendarConsultaResponseContract> Update(int id, AgendarConsultaRequestContract model)
        {
            var consulta = await _consultaRepository.GetById(id);
            _mapper.Map(model, consulta);
            await _consultaRepository.Update(consulta);
            return _mapper.Map<AgendarConsultaResponseContract>(consulta);
        }
        public async Task<IEnumerable<DateTime>> GetAllDatesOcupied()
        {
            var consultas = await _consultaRepository.GetAllScheduled();
            return consultas.Select(consulta => consulta.Data);
        }
    }
}