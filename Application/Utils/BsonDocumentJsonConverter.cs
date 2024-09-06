using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Application.Utils
{
    public class BsonDocumentJsonConverter : JsonConverter<BsonDocument>
    {
        public override BsonDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                return BsonDocument.Parse(jsonDoc.RootElement.GetRawText());
            }
        }

        public override void Write(Utf8JsonWriter writer, BsonDocument value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(value.ToJson());
        }
    }

}
