using System.Xml.Serialization;

namespace HeracleumSosnowskyiService.Models.Xml
{
    [XmlRoot("LANDSAT_METADATA_FILE", IsNullable = false)]
    public class LandsatMetadata
    {
        [XmlElement(ElementName = "PRODUCT_CONTENTS")]
        public ProductContent? ProductContents;

        [XmlElement(ElementName = "IMAGE_ATTRIBUTES")]
        public ImageAttribute? ImageAttributes;

        [XmlElement(ElementName = "PROJECTION_ATTRIBUTES")]
        public ProjectionAttribute? ProjectionAttributes;
    }
}
