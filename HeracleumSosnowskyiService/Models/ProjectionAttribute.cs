using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace HeracleumSosnowskyiService.Models
{
    public class ProjectionAttribute
    {
        [Key]
        [XmlIgnore]
        public Ulid Id { get; } = Ulid.NewUlid();

        [XmlIgnore]
        public LandsatMetadata? LandsatMetadata { get; set; }

        [XmlElement(ElementName = "MAP_PROJECTION")]
        public string? MapProjection { set; get; }
        [XmlElement(ElementName = "DATUM")]
        public string? Datum { set; get; }
        [XmlElement(ElementName = "ELLIPSOID")]
        public string? Ellipsoid { set; get; }
        [XmlElement(ElementName = "UTM_ZONE")]
        public string? UtmZone { set; get; }
        [XmlElement(ElementName = "GRID_CELL_SIZE_REFLECTIVE")]
        public string? GridCellSizeReflective { set; get; }
        [XmlElement(ElementName = "GRID_CELL_SIZE_THERMAL")]
        public string? GridCellSizeThermal { set; get; }
        [XmlElement(ElementName = "REFLECTIVE_LINES")]
        public string? ReflectiveLines { set; get; }
        [XmlElement(ElementName = "REFLECTIVE_SAMPLES")]
        public string? ReflectiveSamples { set; get; }
        [XmlElement(ElementName = "THERMAL_LINES")]
        public string? ThermalLines { set; get; }
        [XmlElement(ElementName = "THERMAL_SAMPLES")]
        public string? ThermalSamples { set; get; }
        [XmlElement(ElementName = "ORIENTATION")]
        public string? Orientation { set; get; }
        [XmlElement(ElementName = "CORNER_UL_LAT_PRODUCT")]
        public string? CornerUlLatProduct { set; get; }
        [XmlElement(ElementName = "CORNER_UL_LON_PRODUCT")]
        public string? CornerUlLonProduct { set; get; }
        [XmlElement(ElementName = "CORNER_UR_LAT_PRODUCT")]
        public string? CornerUrLatProduct { set; get; }
        [XmlElement(ElementName = "CORNER_UR_LON_PRODUCT")]
        public string? CornerUrLonProduct { set; get; }
        [XmlElement(ElementName = "CORNER_LL_LAT_PRODUCT")]
        public string? CornerLlLatProduct { set; get; }
        [XmlElement(ElementName = "CORNER_LL_LON_PRODUCT")]
        public string? CornerLlLonProduct { set; get; }
        [XmlElement(ElementName = "CORNER_LR_LAT_PRODUCT")]
        public string? CornerLrLatProduct { set; get; }
        [XmlElement(ElementName = "CORNER_LR_LON_PRODUCT")]
        public string? CornerLrLonProduct { set; get; }
        [XmlElement(ElementName = "CORNER_UL_PROJECTION_X_PRODUCT")]
        public string? CornerUlProjectionXProduct { set; get; }
        [XmlElement(ElementName = "CORNER_UL_PROJECTION_Y_PRODUCT")]
        public string? CornerUlProjectionYProduct { set; get; }
        [XmlElement(ElementName = "CORNER_UR_PROJECTION_X_PRODUCT")]
        public string? CornerUrProjectionXProduct { set; get; }
        [XmlElement(ElementName = "CORNER_UR_PROJECTION_Y_PRODUCT")]
        public string? CornerUrProjectionYProduct { set; get; }
        [XmlElement(ElementName = "CORNER_LL_PROJECTION_X_PRODUCT")]
        public string? CornerLlProjectionXProduct { set; get; }
        [XmlElement(ElementName = "CORNER_LL_PROJECTION_Y_PRODUCT")]
        public string? CornerLlProjectionYProduct { set; get; }
        [XmlElement(ElementName = "CORNER_LR_PROJECTION_X_PRODUCT")]
        public string? CornerLrProjectionXProduct { set; get; }
        [XmlElement(ElementName = "CORNER_LR_PROJECTION_Y_PRODUCT")]
        public string? CornerLrProjectionYProduct { set; get; }
    }
}
