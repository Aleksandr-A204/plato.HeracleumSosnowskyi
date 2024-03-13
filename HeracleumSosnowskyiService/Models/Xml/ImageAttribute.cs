using System.Xml.Serialization;

namespace HeracleumSosnowskyiService.Models.Xml
{
    public class ImageAttribute
    {
        [XmlElement(ElementName = "SPACECRAFT_ID")]
        public string? SpacecraftId;
        [XmlElement(ElementName = "SENSOR_ID")]
        public string? SensorId;
        [XmlElement(ElementName = "WRS_TYPE")]
        public string? WrsType;
        [XmlElement(ElementName = "WRS_PATH")]
        public string? WrsPath;
        [XmlElement(ElementName = "WRS_ROW")]
        public string? WrsRow;
        [XmlElement(ElementName = "NADIR_OFFNADIR")]
        public string? NadirOffnadir;
        [XmlElement(ElementName = "TARGET_WRS_PATH")]
        public string? TargetWrsPath;
        [XmlElement(ElementName = "TARGET_WRS_ROW")]
        public string? TargetWrsRow;
        [XmlElement(ElementName = "DATE_ACQUIRED")]
        public string? DateAcquired;
        [XmlElement(ElementName = "SCENE_CENTER_TIME")]
        public string? SceneCenterTime;
        [XmlElement(ElementName = "STATION_ID")]
        public string? StationId;
        [XmlElement(ElementName = "CLOUD_COVER")]
        public string? CloudCover;
        [XmlElement(ElementName = "CLOUD_COVER_LAND")]
        public string? CloudCoverLand;
        [XmlElement(ElementName = "IMAGE_QUALITY_OLI")]
        public string? ImageQualityOli;
        [XmlElement(ElementName = "IMAGE_QUALITY_TIRS")]
        public string? ImageQualityTirs;
        [XmlElement(ElementName = "SATURATION_BAND_1")]
        public string? SaturationBand1;
        [XmlElement(ElementName = "SATURATION_BAND_2")]
        public string? SaturationBand2;
        [XmlElement(ElementName = "SATURATION_BAND_3")]
        public string? SaturationBand3;
        [XmlElement(ElementName = "SATURATION_BAND_4")]
        public string? SaturationBand4;
        [XmlElement(ElementName = "SATURATION_BAND_5")]
        public string? SaturationBand5;
        [XmlElement(ElementName = "SATURATION_BAND_6")]
        public string? SaturationBand6;
        [XmlElement(ElementName = "SATURATION_BAND_7")]
        public string? SaturationBand7;
        [XmlElement(ElementName = "SATURATION_BAND_8")]
        public string? SaturationBand8;
        [XmlElement(ElementName = "SATURATION_BAND_9")]
        public string? SaturationBand9;
        [XmlElement(ElementName = "ROLL_ANGLE")]
        public string? RollAngle;
        [XmlElement(ElementName = "SUN_AZIMUTH")]
        public string? SunAzimuth;
        [XmlElement(ElementName = "SUN_ELEVATION")]
        public string? SunElevation;
        [XmlElement(ElementName = "EARTH_SUN_DISTANCE")]
        public string? EarthSunDistance;
    }
}
