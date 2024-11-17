using Application.DTOS.Exame;

namespace Application.Services.Interfaces
{
    public interface IExameService: IService<AgendarExameRequestContract, AgendarExameResponseContract, int>
    {
        Task<IEnumerable<AgendarExameResponseContract>?> GetAllScheduled();
        Task<IEnumerable<ExameConcluidoResponse>?> GetAllCompleted();
        Task<IEnumerable<DateTime>> GetAllDatesOcupied();
        Task<ExameConcluidoResponse> SetExamAsCompleted(int id);
        Task<ExameConcluidoResponse> AddExternURL(int id, string url);
        Task<AgendarExameResponseContract> UpdatePaymentStatus(int id);
        Task<IEnumerable<AgendarExameResponseContract>?> GetAllPatientExamsScheduled(int id);
        Task<IEnumerable<ExameConcluidoResponse>?> GetAllPatientExamsCompleted(int id);
        Task<IEnumerable<AgendarExameResponseContract>?> GetAllDoctorExamsScheduled(int id);
        Task<IEnumerable<ExameConcluidoResponse>?> GetAllDoctorExamsCompleted(int id);
        Task<AdicionarResultadoResponseContract> AttachResult(AdicionarResultadoRequestContract model);
    }
}
