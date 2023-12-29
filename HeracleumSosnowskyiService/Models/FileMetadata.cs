using HeracleumSosnowskyiService.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeracleumSosnowskyiService.Models
{
    public class FileMetadata
    {
        [Key]
        public Ulid Id { get; } = Ulid.NewUlid();

        [ForeignKey("SatelliteData")]
        public Ulid SatelliteDataId { get; set; }
        public SatelliteDataOfSpacesystem? SatelliteData { get; set; }

        [ForeignKey("FileInfo")]
        public Ulid FileInfoId { get; set; }
        public FileInfoApi? FileInfo { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? FileStreamId { get; set; }
    }
}
