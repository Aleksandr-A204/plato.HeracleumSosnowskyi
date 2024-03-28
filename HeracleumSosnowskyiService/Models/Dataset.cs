using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HeracleumSosnowskyiService.Models
{
    [Index(nameof(FileStreamId), IsUnique = true)]
    public class Dataset
    {
        [Key]
        public Ulid Id { get; } = Ulid.NewUlid();

        public string? FileStreamId { get; set; }

        [ForeignKey("LandsatMetadata")]
        public Ulid LandsatMetadataId { get; set; }

        public LandsatMetadata? LandsatMetadata { get; set; }

        [ForeignKey("FileInfo")]
        public Ulid FileInfoId { get; set; }

        public FileInfoApi? FileInfo { get; set; }
    }
}
