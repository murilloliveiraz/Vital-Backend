using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    public class ProntuarioRegistro
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int ProntuarioId { get; set; }
        public string Tipo { get; set; }
        public DateTime Data { get; set; }
        [BsonSerializer(typeof(MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer))]
        public BsonDocument Conteudo { get; set; }
    }
}
