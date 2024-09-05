using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Classes
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _context;

        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Administrador> Create(Administrador model)
        {
            await _context.Administradores.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public Task Delete(Administrador model)
        {
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Administrador?>> Get()
        {
            return await _context.Administradores.AsNoTracking().OrderBy(a => a.Id).Include(a => a.Usuario)
           .ToListAsync();
        }

         public async Task<Administrador?> GetById(int id)
        {
            return await _context.Administradores.AsNoTracking().Include(a => a.Usuario).FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<Administrador> Update(Administrador model)
        {
            Administrador administradorAtDatabase = await _context.Administradores.FirstOrDefaultAsync(p => p.Id == model.Id);

            _context.Entry(administradorAtDatabase).CurrentValues.SetValues(model);

            await _context.SaveChangesAsync();
            return administradorAtDatabase;
        }
    }
}