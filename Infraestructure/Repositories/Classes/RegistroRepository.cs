using Domain;
using Infraestructure.Helpers;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infraestructure.Repositories.Classes
{
    public class RegistroRepository : IRegistroRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ProntuarioRegistro> _registros;
        public RegistroRepository(IMongoClient mongoClient, IOptions<MongoDatabaseConfig> mongoConfig)
        {
            _mongoClient = mongoClient;
            _mongoDatabase = _mongoClient.GetDatabase(mongoConfig.Value.DatabaseName);
            _registros = _mongoDatabase.GetCollection<ProntuarioRegistro>("Registros");
        }

        public async Task CreateRecord(ProntuarioRegistro registro)
        {
            await _registros.InsertOneAsync(registro);
        }

        public async Task<ICollection<ProntuarioRegistro>> GetAllRecords(int prontuarioId)
        {
            var registros = await _registros.Find(r => r.ProntuarioId == prontuarioId).ToListAsync();
            return registros;
        }
    }
}
