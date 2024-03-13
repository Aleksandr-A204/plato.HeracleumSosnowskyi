using System.Xml.Serialization;

namespace HeracleumSosnowskyiService.Models.Xml
{
    public class ProjectionAttribute
    {
        [XmlElement(ElementName = "MAP_PROJECTION")]
        public string? MapProjection;
        [XmlElement(ElementName = "DATUM")]
        public string? Datum;
        [XmlElement(ElementName = "ELLIPSOID")]
        public string? Ellipsoid;
        [XmlElement(ElementName = "UTM_ZONE")]
        public string? UtmZone;
        [XmlElement(ElementName = "GRID_CELL_SIZE_REFLECTIVE")]
        public string? GridCellSizeReflective;
        [XmlElement(ElementName = "GRID_CELL_SIZE_THERMAL")]
        public string? GridCellSizeThermal;
        [XmlElement(ElementName = "REFLECTIVE_LINES")]
        public string? ReflectiveLines;
        [XmlElement(ElementName = "REFLECTIVE_SAMPLES")]
        public string? ReflectiveSamples;
        [XmlElement(ElementName = "THERMAL_LINES")]
        public string? ThermalLines;
        [XmlElement(ElementName = "THERMAL_SAMPLES")]
        public string? ThermalSamples;
        [XmlElement(ElementName = "ORIENTATION")]
        public string? Orientation;
        [XmlElement(ElementName = "CORNER_UL_LAT_PRODUCT")]
        public string? CornerUlLatProduct;
        [XmlElement(ElementName = "CORNER_UL_LON_PRODUCT")]
        public string? CornerUlLonProduct;
        [XmlElement(ElementName = "CORNER_UR_LAT_PRODUCT")]
        public string? CornerUrLatProduct;
        [XmlElement(ElementName = "CORNER_UR_LON_PRODUCT")]
        public string? CornerUrLonProduct;
        [XmlElement(ElementName = "CORNER_LL_LAT_PRODUCT")]
        public string? CornerLlLatProduct;
        [XmlElement(ElementName = "CORNER_LL_LON_PRODUCT")]
        public string? CornerLlLonProduct;
        [XmlElement(ElementName = "CORNER_LR_LAT_PRODUCT")]
        public string? CornerLrLatProduct;
        [XmlElement(ElementName = "CORNER_LR_LON_PRODUCT")]
        public string? CornerLrLonProduct;
        [XmlElement(ElementName = "CORNER_UL_PROJECTION_X_PRODUCT")]
        public string? CornerUlProjectionXProduct;
        [XmlElement(ElementName = "CORNER_UL_PROJECTION_Y_PRODUCT")]
        public string? CornerUlProjectionYProduct;
        [XmlElement(ElementName = "CORNER_UR_PROJECTION_X_PRODUCT")]
        public string? CornerUrProjectionXProduct;
        [XmlElement(ElementName = "CORNER_UR_PROJECTION_Y_PRODUCT")]
        public string? CornerUrProjectionYProduct;
        [XmlElement(ElementName = "CORNER_LL_PROJECTION_X_PRODUCT")]
        public string? CornerLlProjectionXProduct;
        [XmlElement(ElementName = "CORNER_LL_PROJECTION_Y_PRODUCT")]
        public string? CornerLlProjectionYProduct;
        [XmlElement(ElementName = "CORNER_LR_PROJECTION_X_PRODUCT")]
        public string? CornerLrProjectionXProduct;
        [XmlElement(ElementName = "CORNER_LR_PROJECTION_Y_PRODUCT")]
        public string? CornerLrProjectionYProduct;
    }
}
