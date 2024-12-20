using Application.DTOS.Admin;

namespace Application.Services.Interfaces
{
    public interface IAdminService : IService<AdminRequestContract, AdminResponseContract, int>
    {
        Task<AdminResponseContract?> GetByEmail(string email);
    }
}