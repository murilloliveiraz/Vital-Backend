using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Classes
{
    public class HospitalServicoRepository : IHospitalServicoRepository
    {
        private readonly ApplicationContext _context;

        public HospitalServicoRepository(ApplicationContext context)
        {
            _context = context;
        }

         public async Task<HospitalServico> Create(HospitalServico model)
        {
            await _context.HospitalServicos.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<HospitalServico> GetByHospitalIdAndServicoId(int hospitalId, int servicoId)
        {
            return await _context.HospitalServicos.Include(hs => hs.Servico)
                .FirstOrDefaultAsync(hs => hs.HospitalId == hospitalId && hs.ServicoId == servicoId);
        }

        public async Task Delete(HospitalServico hospitalServico)
        {
            _context.HospitalServicos.Remove(hospitalServico);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HospitalServico?>> GetAllByHospitalId(int hospitalId)
        {
            return await _context.HospitalServicos.Include(hs => hs.Servico)
           .Where(hs => hs.HospitalId == hospitalId)
           .ToListAsync();
        }

        public async Task<IEnumerable<HospitalServico?>> GetAllHospitalsThatOfferAnSpecificService(int servicoId)
        {
            return await _context.HospitalServicos.Include(hs => hs.Hospital)
          .Where(hs => hs.ServicoId == servicoId)
          .ToListAsync();
        }
    }
}