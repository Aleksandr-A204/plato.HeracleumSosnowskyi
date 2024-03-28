using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace HeracleumSosnowskyiService.Models
{
    [Index(nameof(FileName), IsUnique = true)]
    public class FileInfoApi
    {
        [Key]
        public Ulid Id { get; } = Ulid.NewUlid();

        [Required(ErrorMessage = "Ошибка запроса. Требуется информация о файле.")]
        public string? FileName { get; set; }

        [Required(ErrorMessage = "Ошибка запроса. Требуется информация о файле.")]
        public string? MimeType { get; set; }

        public long? LastModified { get; set; }

        public string? FileStreamId { get; set; }

        public Dataset? Datasets { get; set; }
    }
}
