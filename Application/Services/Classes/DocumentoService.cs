using Application.DTOS.Consulta;
using Application.DTOS.Exame;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using Infraestructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Classes
{
    public class DocumentoService : IDocumentoService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IDocumentoRepository _documentoRepository;
        private readonly IMapper _mapper;
        private readonly IS3StorageService _s3StorageService;
        private IConfiguration _configuration;
        private readonly IEmailService _emailSender;

        public DocumentoService(
            IMapper mapper,
            IS3StorageService s3StorageService,
            IConfiguration configuration,
            IDocumentoRepository documentoRepository,
            IConsultaRepository consultaRepository,
            IEmailService emailSender)
        {
            _mapper = mapper;
            _s3StorageService = s3StorageService;
            _configuration = configuration;
            _documentoRepository = documentoRepository;
            _consultaRepository = consultaRepository;
            _emailSender = emailSender;
        }
        public async Task<AdicionarDocumentoResponseContract> AttachDocument(AdicionarDocumentoRequestContract model)
        {
            var consulta = await _consultaRepository.GetById(model.ConsultaId);
            if (consulta == null)
            {
                throw new Exception("Consulta not found.");
            }

            var bucketName = _configuration["S3Storage:BucketName"];
            
            var resultUpload = await _s3StorageService.UploadFileAsync(model.File, consulta.PrefixoDaPasta, bucketName);
            
            if (!resultUpload.Success)
            {
                throw new Exception("File upload failed.");
            }

            var document = new Documento
            {
                S3KeyPath = resultUpload.Key,
                ConsultaId = model.ConsultaId
            };
            
            await _documentoRepository.Create(document);
            
            var mailRequest = new MailRequest
            {
                ToEmail = consulta.EmailParaReceberNotificacoes,
                Subject = "Um novo documento foi anexado à sua consulta!",
                Body = CommunicationEmail.ResultAvailableEmail("http://localhost:4200")
            };

            await _emailSender.SendEmailAsync(mailRequest);
            
            return _mapper.Map<AdicionarDocumentoResponseContract>(document);
        }

    }
}