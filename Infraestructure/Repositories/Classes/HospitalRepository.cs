using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Classes
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly ApplicationContext _context;

        public HospitalRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Hospital> Create(Hospital model)
        {
            await _context.Hospitais.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(Hospital model)
        {
            model.DataInativacao = DateTime.UtcNow;
            await Update(model);
        }

        public async Task<IEnumerable<Hospital?>> Get()
        {
            return await _context.Hospitais.AsNoTracking()
           .OrderBy(h => h.Id)
           .ToListAsync();
        }

        public async Task<IEnumerable<Hospital>?> GetAllByLocation(string estado)
        {
            return await _context.Hospitais.AsNoTracking().Where(h => h.Estado == estado)
           .OrderBy(h => h.Id)
           .ToListAsync();
        }

        public async Task<Hospital?> GetById(int id)
        {
            return await _context.Hospitais.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Hospital?> GetByName(string name)
        {
            return await _context.Hospitais.AsNoTracking().FirstOrDefaultAsync(h => h.Nome == name);
        }

        public async Task<Hospital> Update(Hospital model)
        {
            Hospital hospitalAtDatabase = await _context.Hospitais.FirstOrDefaultAsync(h => h.Id == model.Id);

            _context.Entry(hospitalAtDatabase).CurrentValues.SetValues(model);

            await _context.SaveChangesAsync();
            return hospitalAtDatabase;
        }
    }
}