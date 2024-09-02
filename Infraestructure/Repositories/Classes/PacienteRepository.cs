using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Classes
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ApplicationContext _context;

        public PacienteRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Paciente> Create(Paciente model)
        {
            await _context.Pacientes.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public Task Delete(Paciente model)
        {
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Paciente?>> Get()
        {
            return await _context.Pacientes.AsNoTracking().OrderBy(p => p.Id).Include(p => p.Usuario)
           .ToListAsync();
        }

        public async Task<Paciente?> GetByCPF(string cpf)
        {
            return await _context.Pacientes.AsNoTracking().Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Usuario.CPF == cpf);
        }

        public async Task<Paciente?> GetByEmail(string email)
        {
            return await _context.Pacientes.AsNoTracking().Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Usuario.Email == email);
        }

        public async Task<Paciente?> GetById(int id)
        {
            return await _context.Pacientes.AsNoTracking().Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paciente?> GetByName(string name)
        {
            return await _context.Pacientes.AsNoTracking().Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Usuario.Nome == name);
        }

        public async Task<Paciente> Update(Paciente model)
        {
            Paciente pacienteAtDatabase = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == model.Id);

            _context.Entry(pacienteAtDatabase).CurrentValues.SetValues(model);

            await _context.SaveChangesAsync();
            return pacienteAtDatabase;
        }
    }
}