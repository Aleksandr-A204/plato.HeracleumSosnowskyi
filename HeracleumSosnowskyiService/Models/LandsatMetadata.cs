using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace HeracleumSosnowskyiService.Models
{
    [XmlRoot("LANDSAT_METADATA_FILE", IsNullable = false)]
    public class LandsatMetadata
    {
        [Key]
        [XmlIgnore]
        public Ulid Id { get; } = Ulid.NewUlid();

        [ForeignKey("ProductContents")]
        [XmlIgnore]
        public Ulid ProductContentId { get; set; }

        [ForeignKey("ImageAttributes")]
        [XmlIgnore]
        public Ulid ImageAttributeId { get; set; }

        [ForeignKey("ProjectionAttributes")]
        [XmlIgnore]
        public Ulid ProjectionAttributeId { get; set; }

        [XmlElement(ElementName = "PRODUCT_CONTENTS")]
        public ProductContent? ProductContents { get; set; }

        [XmlElement(ElementName = "IMAGE_ATTRIBUTES")]
        public ImageAttribute? ImageAttributes { get; set; }

        [XmlElement(ElementName = "PROJECTION_ATTRIBUTES")]
        public ProjectionAttribute? ProjectionAttributes { get; set; }

        [XmlIgnore]
        public ICollection<Dataset> Datasets { get; set; } = new List<Dataset>();
    }
}
