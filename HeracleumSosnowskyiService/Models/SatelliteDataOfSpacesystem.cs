using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace HeracleumSosnowskyiService.Models
{
    [DataContract]
    public class SatelliteDataOfSpacesystem 
    {
        [Key]
        [DataMember(Name = "id")]
        public Ulid Id { get; } = Ulid.NewUlid();

        [DataMember(Name = "landsatProductId")]
        public string? LandsatProductId { get; set; }

        [DataMember(Name = "datasets")]
        public ICollection<Dataset>? Datasets { get; set; } = new List<Dataset>();

    }
}
