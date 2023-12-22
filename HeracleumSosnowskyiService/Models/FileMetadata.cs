using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HeracleumSosnowskyiService.Models
{
    public class FileMetadata
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; }

        [BsonElement("satelliteImagesDataset_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId SatelliteImagesDatasetId { get; set; }

        [BsonElement("fileInfo_Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId FileInfoId { get; set; }

        [BsonElement("fileStream_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId FileStreamId { get; set; }
    }
}
