using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HeracleumSosnowskyiService.Models
{
    [DataContract]
    public class Dataset
    {
        [Key]
        [DataMember(Name = "id")]
        public Ulid Id { get; } = Ulid.NewUlid();

        [ForeignKey("SatelliteData")]
        [DataMember(Name = "satelliteDataId")]
        public Ulid SatelliteDataId { get; set; }

        [DataMember(Name = "satelliteData")]
        public SatelliteDataOfSpacesystem? SatelliteData { get; set; }

        [ForeignKey("FileInfo")]
        [DataMember(Name = "fileInfoId")]
        public Ulid FileInfoId { get; set; }

        [DataMember(Name = "fileInfo")]
        public FileInfoApi? FileInfo { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember(Name = "fileStreamId")]
        public string? FileStreamId { get; set; }
    }
}
