using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories.Classes
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private readonly ApplicationContext _context;

        public DocumentoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Documento?> Create(Documento model)
        {
            await _context.Documentos.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}