using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Classes
{
    public class ExameRepository : IExameRepository
    {
        private readonly ApplicationContext _context;

        public ExameRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Exame> Create(Exame model)
        {
            await _context.Exames.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(Exame model)
        {
            model.Status = "Cancelado";
            await Update(model);
        }

        public async Task<IEnumerable<Exame?>> Get()
        {
            return await _context.Exames.AsNoTracking()
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<IEnumerable<Exame>?> GetAllCompleted()
        {
            return await _context.Exames.AsNoTracking().Where(e => e.Status == "Concluido")
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<IEnumerable<Exame>?> GetAllPatientExamsCompleted(int id)
        {
            return await _context.Exames.AsNoTracking().Where(e => e.PacienteId == id && e.Status == "Concluido")
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<IEnumerable<Exame>?> GetAllPatientExamsScheduled(int id)
        {
            return await _context.Exames.AsNoTracking().Where(e => e.PacienteId == id && e.Status == "Agendado")
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<IEnumerable<Exame>?> GetAllScheduled()
        {
            return await _context.Exames.AsNoTracking().Where(e => e.Status == "Agendado")
           .OrderByDescending(e => e.Data)
           .ToListAsync();
        }

        public async Task<Exame?> GetById(int id)
        {
            return await _context.Exames.AsNoTracking().FirstOrDefaultAsync(e => e.ExameId == id);
        }

        public async Task<Exame> Update(Exame model)
        {
            Exame exameAtDatabase = await _context.Exames.FirstOrDefaultAsync(e => e.ExameId == model.ExameId);

            _context.Entry(exameAtDatabase).CurrentValues.SetValues(model);

            await _context.SaveChangesAsync();
            return exameAtDatabase;
        }
    }
}
