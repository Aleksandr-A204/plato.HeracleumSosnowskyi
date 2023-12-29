using HeracleumSosnowskyiService.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace HeracleumSosnowskyiService.Models
{
    public class SatelliteDataOfSpacesystem 
    {
        [Key]
        public Ulid Id { get; } = Ulid.NewUlid();

        public string? LandsatProductId { get; set; }

        public ICollection<FileMetadata>? Metadata { get; set; } = new List<FileMetadata>();

    }
}
