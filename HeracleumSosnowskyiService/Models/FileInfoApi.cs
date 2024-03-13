using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HeracleumSosnowskyiService.Models
{
    public class FileInfoApi
    {
        [Key]
        public Ulid Id { get; } = Ulid.NewUlid();

        [Required(ErrorMessage = "Ошибка запроса. Требуется информация о файле.")]
        public string? FileName { get; set; }

        [Required(ErrorMessage = "Ошибка запроса. Требуется информация о файле.")]
        public string? MimeType { get; set; }

        public long LastModified { get; set; } = 0;

        public string? FileStreamId { get; set; }

        public Dataset? Datasets { get; set; }
    }
}
