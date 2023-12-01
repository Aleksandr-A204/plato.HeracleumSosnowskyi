using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HeracleumSosnowskyiService.Models
{
    public class FileInfoApi
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Ошибка запроса. Требуется информация о файле.")]
        public string? FileName { get; set; }

        [BsonElement("MIME-type")]
        [Required(ErrorMessage = "Ошибка запроса. Требуется информация о файле.")]
        public string? MimeType { get; set; }

        public long LastModified { get; set; } = 0;

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId FileStreamId { get; set; }
    }
}
