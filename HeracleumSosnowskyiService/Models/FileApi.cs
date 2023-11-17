using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HeracleumSosnowskyiService.Models
{
    public class FileApi
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Ошибка запроса. Требуется информация о файле.")]
        public string? FileName { get; set; }

        [Required(ErrorMessage = "Ошибка запроса. Требуется информация о файле.")]
        public string? Type { get; set; }
        public long LastModified { get; set; } = 0;

    }
}
