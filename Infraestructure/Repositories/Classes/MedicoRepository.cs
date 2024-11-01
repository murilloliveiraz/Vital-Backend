using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Classes
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly ApplicationContext _context;

        public MedicoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Medico> Create(Medico model)
        {
            await _context.Medicos.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(Medico model)
        {
            model.Usuario.DataInativacao = DateTime.UtcNow;
            await Update(model);
        }

        public async Task<IEnumerable<Medico?>> Get()
        {
            return await _context.Medicos.AsNoTracking().OrderBy(m => m.Id).Include(m => m.Usuario)
           .ToListAsync();
        }

        public async Task<IEnumerable<Medico>?> GetAllBySpecialization(string especialization)
        {
            return await _context.Medicos.AsNoTracking().Where(m => m.Especialidade == especialization).OrderBy(m => m.Id).Include(m => m.Usuario)
           .ToListAsync();
        }

        public async Task<Medico?> GetByEmail(string email)
        {
            return await _context.Medicos.AsNoTracking().Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Usuario.Email == email);
        }

        public async Task<Medico?> GetByCRM(string crm)
        {
            return await _context.Medicos.AsNoTracking().Include(m => m.Usuario).FirstOrDefaultAsync(m => m.CRM == crm);
        }

        public async Task<Medico?> GetById(int id)
        {
            return await _context.Medicos.AsNoTracking().Include(m => m.Usuario).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Medico> Update(Medico model)
        {
            Medico medicoAtDatabase = await _context.Medicos.FirstOrDefaultAsync(m => m.Id == model.Id);

            _context.Entry(medicoAtDatabase).CurrentValues.SetValues(model);

            await _context.SaveChangesAsync();
            return medicoAtDatabase;
        }
    }
}