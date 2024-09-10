using Application.DTOS.Exame;
using Application.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using Infraestructure.Services.Interfaces;

namespace Application.Services.Classes
{
    public class ExameService : IExameService
    {
        private readonly IExameRepository _exameRepository;
        private readonly IMapper _mapper;
        private readonly IS3StorageService _s3StorageService;
        public ExameService(IMapper mapper, IExameRepository exameRepository, IS3StorageService s3StorageService)
        {
            _mapper = mapper;
            _exameRepository = exameRepository;
            _s3StorageService = s3StorageService;
        }

        public async Task<AdicionarResultadoResponseContract> AttachResult(AdicionarResultadoRequestContract model)
        {
            var exame = await _exameRepository.GetById(model.ExameId);
            var resultUpload = await _s3StorageService.UploadFileAsync(model.File, exame.PrefixoDaPasta);
            if (resultUpload.Success)
            {
                exame.S3KeyPath = resultUpload.Key;
            }
            await _exameRepository.Update(exame);
            return _mapper.Map<AdicionarResultadoResponseContract>(exame);
        }

        public async Task<AgendarExameResponseContract> Create(AgendarExameRequestContract model)
        {
            Exame exame = _mapper.Map<Exame>(model);
            string dataFormatada = exame.Data.ToString("yyyyMMdd");
            exame.PrefixoDaPasta = $"{exame.Paciente.Usuario.CPF}/{dataFormatada}";
            exame = await _exameRepository.Create(exame);
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