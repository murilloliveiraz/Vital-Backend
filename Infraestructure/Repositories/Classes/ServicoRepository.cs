using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Classes
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly ApplicationContext _context;

        public ServicoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Servico> Create(Servico model)
        {
            await _context.Servicos.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(Servico model)
        {
            model.DataInativacao = DateTime.UtcNow;
            await Update(model);
        }

        public async Task<IEnumerable<Servico?>> Get()
        {
            return await _context.Servicos.AsNoTracking().Where(s => s.DataInativacao == null).OrderBy(s => s.ServicoId)
           .ToListAsync();
        }

        public async Task<Servico?> GetById(int id)
        {
            return await _context.Servicos.AsNoTracking().FirstOrDefaultAsync(s => s.ServicoId == id);
        }

        public async Task<Servico?> GetByName(string name)
        {
            return await _context.Servicos.AsNoTracking().FirstOrDefaultAsync(s => s.Nome == name);
        }
        
        public async Task<IEnumerable<Servico?>> GetAllIncludingDeleteds()
        {
            return await _context.Servicos.AsNoTracking().OrderBy(s => s.ServicoId)
           .ToListAsync();
        }

        public async Task<Servico> Update(Servico model)
        {
            Servico servicoAtDatabase = await _context.Servicos.FirstOrDefaultAsync(s => s.ServicoId == model.ServicoId);

            _context.Entry(servicoAtDatabase).CurrentValues.SetValues(model);

            await _context.SaveChangesAsync();
            return servicoAtDatabase;
        }
    }
}