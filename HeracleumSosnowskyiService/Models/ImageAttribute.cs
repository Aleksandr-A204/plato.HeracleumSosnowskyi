using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace HeracleumSosnowskyiService.Models
{
    public class ImageAttribute
    {
        [Key]
        [XmlIgnore]
        public Ulid Id { get; } = Ulid.NewUlid();

        [XmlIgnore]
        public LandsatMetadata? LandsatMetadata { get; set; }

        [XmlElement(ElementName = "SPACECRAFT_ID")]
        public string? SpacecraftId { set; get; }
        [XmlElement(ElementName = "SENSOR_ID")]
        public string? SensorId { set; get; }
        [XmlElement(ElementName = "WRS_TYPE")]
        public string? WrsType { set; get; }
        [XmlElement(ElementName = "WRS_PATH")]
        public string? WrsPath { set; get; }
        [XmlElement(ElementName = "WRS_ROW")]
        public string? WrsRow { set; get; }
        [XmlElement(ElementName = "NADIR_OFFNADIR")]
        public string? NadirOffnadir { set; get; }
        [XmlElement(ElementName = "TARGET_WRS_PATH")]
        public string? TargetWrsPath { set; get; }
        [XmlElement(ElementName = "TARGET_WRS_ROW")]
        public string? TargetWrsRow { set; get; }
        [XmlElement(ElementName = "DATE_ACQUIRED")]
        public string? DateAcquired { set; get; }
        [XmlElement(ElementName = "SCENE_CENTER_TIME")]
        public string? SceneCenterTime { set; get; }
        [XmlElement(ElementName = "STATION_ID")]
        public string? StationId { set; get; }
        [XmlElement(ElementName = "CLOUD_COVER")]
        public string? CloudCover { set; get; }
        [XmlElement(ElementName = "CLOUD_COVER_LAND")]
        public string? CloudCoverLand { set; get; }
        [XmlElement(ElementName = "IMAGE_QUALITY_OLI")]
        public string? ImageQualityOli { set; get; }
        [XmlElement(ElementName = "IMAGE_QUALITY_TIRS")]
        public string? ImageQualityTirs { set; get; }
        [XmlElement(ElementName = "SATURATION_BAND_1")]
        public string? SaturationBand1 { set; get; }
        [XmlElement(ElementName = "SATURATION_BAND_2")]
        public string? SaturationBand2 { set; get; }
        [XmlElement(ElementName = "SATURATION_BAND_3")]
        public string? SaturationBand3 { set; get; }
        [XmlElement(ElementName = "SATURATION_BAND_4")]
        public string? SaturationBand4 { set; get; }
        [XmlElement(ElementName = "SATURATION_BAND_5")]
        public string? SaturationBand5 { set; get; }
        [XmlElement(ElementName = "SATURATION_BAND_6")]
        public string? SaturationBand6 { set; get; }
        [XmlElement(ElementName = "SATURATION_BAND_7")]
        public string? SaturationBand7 { set; get; }
        [XmlElement(ElementName = "SATURATION_BAND_8")]
        public string? SaturationBand8 { set; get; }
        [XmlElement(ElementName = "SATURATION_BAND_9")]
        public string? SaturationBand9 { set; get; }
        [XmlElement(ElementName = "ROLL_ANGLE")]
        public string? RollAngle { set; get; }
        [XmlElement(ElementName = "SUN_AZIMUTH")]
        public string? SunAzimuth { set; get; }
        [XmlElement(ElementName = "SUN_ELEVATION")]
        public string? SunElevation { set; get; }
        [XmlElement(ElementName = "EARTH_SUN_DISTANCE")]
        public string? EarthSunDistance { set; get; }
    }
}
