using Application.DTOS.Consulta;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Google.Apis.Calendar.v3.Data;

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

        public ConsultaService(IMapper mapper, IConsultaRepository consultaRepository, IPacienteService pacienteService, IConfiguration configuration, IEmailService emailSender, IDocumentoService documentoService, IMedicoService medicoService, IGoogleMeetService googleMeetService)
        {
            _mapper = mapper;
            _consultaRepository = consultaRepository;
            _pacienteService = pacienteService;
            _configuration = configuration;
            _emailSender = emailSender;
            _documentoService = documentoService;
            _medicoService = medicoService;
            _googleMeetService = googleMeetService;
        }

        public async Task<AgendarConsultaResponseContract> Create(AgendarConsultaRequestContract model)
        {
            var paciente = await _pacienteService.GetById(model.PacienteId);
            if (paciente == null)
            {
                throw new Exception("Paciente n�o encontrado.");
            }
            Consulta consulta = _mapper.Map<Consulta>(model);
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
                throw new Exception("Paciente nou medico não encontrado.");
            }
            Consulta consulta = _mapper.Map<Consulta>(model);
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

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AgendarConsultaResponseContract>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllCompleted()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllDoctorAppointmentsCompleted(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllDoctorAppointmentsScheduled(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ConsultaConcluidaResponse>?> GetAllPatientAppointmentsCompleted(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllPatientAppointmentsScheduled(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AgendarConsultaResponseContract>?> GetAllScheduled()
        {
            throw new NotImplementedException();
        }

        public async Task<AgendarConsultaResponseContract> GetById(int id)
        {
            var exame = await _consultaRepository.GetById(id);
            return _mapper.Map<AgendarConsultaResponseContract>(exame);
        }

        public Task<AgendarConsultaResponseContract> Update(int id, AgendarConsultaRequestContract model)
        {
            throw new NotImplementedException();
        }
    }
}