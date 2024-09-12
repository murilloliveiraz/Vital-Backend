using Application.DTOS.Exame;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Helpers;
using Infraestructure.Repositories.Classes;
using Infraestructure.Repositories.Interfaces;
using Infraestructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Classes
{
    public class ExameService : IExameService
    {
        private readonly IExameRepository _exameRepository;
        private readonly IPacienteService _pacienteService;
        private readonly IMapper _mapper;
        private readonly IS3StorageService _s3StorageService;
        private IConfiguration _configuration;

        private readonly IEmailService _emailSender;
        public ExameService(IMapper mapper, IExameRepository exameRepository, IS3StorageService s3StorageService, IPacienteService pacienteService, IConfiguration configuration, IEmailService emailSender)
        {
            _mapper = mapper;
            _exameRepository = exameRepository;
            _s3StorageService = s3StorageService;
            _pacienteService = pacienteService;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public async Task<AdicionarResultadoResponseContract> AttachResult(AdicionarResultadoRequestContract model)
        {
            var exame = await _exameRepository.GetById(model.ExameId);
            string base64File = Base64FileConverter.ConvertToBase64(model.File.OpenReadStream());
            var bucketname = _configuration["S3Storage:Bucket-Name"];
            var resultUpload = await _s3StorageService.UploadFileAsync(model.File, exame.PrefixoDaPasta, bucketname);
            if (resultUpload.Success)
            {
                exame.S3KeyPath = resultUpload.Key;
                exame.Base64 = base64File;
            }
            await _exameRepository.Update(exame);
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = exame.EmailParaReceberResultado,
                Subject = "Seu resultado já está disponível",
                Body = CommunicationEmail.ResultAvailableEmail("http://localhost:4200")
            };
            await _emailSender.SendEmailAsync(mailRequest);
            return _mapper.Map<AdicionarResultadoResponseContract>(exame);
        }

        public async Task<AgendarExameResponseContract> Create(AgendarExameRequestContract model)
        {
            var paciente = await _pacienteService.GetById(model.PacienteId);
            if (paciente == null)
            {
                throw new Exception("Paciente não encontrado.");
            }
            Exame exame = _mapper.Map<Exame>(model);
            string dataFormatada = exame.Data.ToString("yyyyMMdd");
            exame.PrefixoDaPasta = $"{paciente.CPF}/{dataFormatada}";
            exame = await _exameRepository.Create(exame);
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = paciente.Email,
                Subject = "Confirmação de agendamento de consulta",
                Body = CommunicationEmail.AppointmentConfirmationEmail(exame.Nome)
            };
            await _emailSender.SendEmailAsync(mailRequest);
            return _mapper.Map<AgendarExameResponseContract>(exame);
        }

        public async Task Delete(int id)
        {
            var exame = await _exameRepository.GetById(id);
            await _exameRepository.Delete(_mapper.Map<Exame>(exame));
        }

        public async Task<IEnumerable<AgendarExameResponseContract>> Get()
        {
            var exames = await _exameRepository.Get();
            return exames.Select(e => _mapper.Map<AgendarExameResponseContract>(e));
        }

        public async Task<IEnumerable<ExameConcluidoResponse>?> GetAllCompleted()
        {
            var exames = await _exameRepository.GetAllCompleted();
            return exames.Select(e => _mapper.Map<ExameConcluidoResponse>(e));
        }

        public async Task<IEnumerable<ExameConcluidoResponse>?> GetAllPatientExamsCompleted(int id)
        {
            var exames = await _exameRepository.GetAllPatientExamsCompleted(id);
            return exames.Select(e => _mapper.Map<ExameConcluidoResponse>(e));
        }

        public async Task<IEnumerable<AgendarExameResponseContract>?> GetAllPatientExamsScheduled(int id)
        {
            var exames = await _exameRepository.GetAllPatientExamsScheduled(id);
            return exames.Select(e => _mapper.Map<AgendarExameResponseContract>(e)); 
        }

        public async Task<IEnumerable<AgendarExameResponseContract>?> GetAllScheduled()
        {
            var exames = await _exameRepository.GetAllScheduled();
            return exames.Select(e => _mapper.Map<AgendarExameResponseContract>(e)); 
        }

        public async Task<AgendarExameResponseContract> GetById(int id)
        {
            var exame = await _exameRepository.GetById(id);          
            return _mapper.Map<AgendarExameResponseContract>(exame); 
        }

        public async Task<AgendarExameResponseContract> Update(int id, AgendarExameRequestContract model)
        {
            var exame = await _exameRepository.GetById(id);
            _mapper.Map(model, exame);
            await _exameRepository.Update(exame);
            return _mapper.Map<AgendarExameResponseContract>(exame);
        }
    }
}