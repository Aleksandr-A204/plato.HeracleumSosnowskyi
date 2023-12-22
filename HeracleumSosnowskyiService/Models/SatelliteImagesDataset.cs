using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HeracleumSosnowskyiService.Models
{
    public class SatelliteImagesDataset
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; }

        [BsonElement("landsatProductId")]
        public string? LandsatProductId { get; set; }
    }
}
