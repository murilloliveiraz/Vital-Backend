using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories.Classes
{
    public class ProntuarioRepository : IProntuarioRepository
    {
        private readonly ApplicationContext _context;

        public ProntuarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Prontuario> Create(Prontuario prontuario)
        {
            _context.Prontuarios.Add(prontuario);
            await _context.SaveChangesAsync();
            return prontuario;
        }
    }
}
