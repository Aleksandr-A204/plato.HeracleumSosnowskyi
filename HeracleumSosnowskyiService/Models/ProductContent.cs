using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace HeracleumSosnowskyiService.Models
{
    public class ProductContent
    {
        [Key]
        [XmlIgnore]
        public Ulid Id { get; } = Ulid.NewUlid();

        [XmlIgnore]
        public LandsatMetadata? LandsatMetadata { get; set; }

        [XmlElement(ElementName = "ORIGIN")]
        public string? Origin { set; get; }
        [XmlElement(ElementName = "DIGITAL_OBJECT_IDENTIFIER")]
        public string? DigitalObjectIdentifier { set; get; }
        [XmlElement(ElementName = "LANDSAT_PRODUCT_ID")]
        public string? LandsatProductId { set; get; }
        [XmlElement(ElementName = "PROCESSING_LEVEL")]
        public string? ProcessingLevel { set; get; }
        [XmlElement(ElementName = "COLLECTION_NUMBER")]
        public string? CollectionNumber { set; get; }
        [XmlElement(ElementName = "COLLECTION_CATEGORY")]
        public string? CollectionCategory { set; get; }
        [XmlElement(ElementName = "OUTPUT_FORMAT")]
        public string? Output_format { set; get; }
        [XmlElement(ElementName = "FILE_NAME_BAND_1")]
        public string? FileNameBand1 { set; get; }
        [XmlElement(ElementName = "FILE_NAME_BAND_2")]
        public string? FileNameBand2 { set; get; }
        [XmlElement(ElementName = "FILE_NAME_BAND_3")]
        public string? FileNameBand3 { set; get; }
        [XmlElement(ElementName = "FILE_NAME_BAND_4")]
        public string? FileNameBand4 { set; get; }
        [XmlElement(ElementName = "FILE_NAME_BAND_5")]
        public string? FileNameBand5 { set; get; }
        [XmlElement(ElementName = "FILE_NAME_BAND_6")]
        public string? FileNameBand6 { set; get; }
        [XmlElement(ElementName = "FILE_NAME_BAND_7")]
        public string? FileNameBand7 { set; get; }
        [XmlElement(ElementName = "FILE_NAME_BAND_ST_B10")]
        public string? FileNameBandStB10 { set; get; }
        [XmlElement(ElementName = "FILE_NAME_THERMAL_RADIANCE")]
        public string? FileNameThermalRadiance { set; get; }
        [XmlElement(ElementName = "FILE_NAME_UPWELL_RADIANCE")]
        public string? FileNameUpwellRadiance { set; get; }
        [XmlElement(ElementName = "FILE_NAME_DOWNWELL_RADIANCE")]
        public string? FileNameDownwellRadiance { set; get; }
        [XmlElement(ElementName = "FILE_NAME_ATMOSPHERIC_TRANSMITTANCE")]
        public string? FileNameAtmosphericTransmittance { set; get; }
        [XmlElement(ElementName = "FILE_NAME_EMISSIVITY")]
        public string? FileNameEmissivity { set; get; }
        [XmlElement(ElementName = "FILE_NAME_EMISSIVITY_STDEV")]
        public string? FileNameEmissivityStdev { set; get; }
        [XmlElement(ElementName = "FILE_NAME_CLOUD_DISTANCE")]
        public string? FileNameCloudDistance { set; get; }
        [XmlElement(ElementName = "FILE_NAME_QUALITY_L2_AEROSOL")]
        public string? FileNameQualityL2Aerosol { set; get; }
        [XmlElement(ElementName = "FILE_NAME_QUALITY_L2_SURFACE_TEMPERATURE")]
        public string? FileNameQualityL2SurfaceTemperature { set; get; }
        [XmlElement(ElementName = "FILE_NAME_QUALITY_L1_PIXEL")]
        public string? FileNameQualityL1Pixel { set; get; }
        [XmlElement(ElementName = "FILE_NAME_QUALITY_L1_RADIOMETRIC_SATURATION")]
        public string? FileNameQualityL1RadiometricSaturation { set; get; }
        [XmlElement(ElementName = "FILE_NAME_ANGLE_COEFFICIENT")]
        public string? FileNameAngleCoefficient { set; get; }
        [XmlElement(ElementName = "FILE_NAME_METADATA_ODL")]
        public string? FileNameMetadataOdl { set; get; }
        [XmlElement(ElementName = "FILE_NAME_METADATA_XML")]
        public string? FileNameMetadataXml { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_BAND_1")]
        public string? DataTypeBand1 { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_BAND_2")]
        public string? DataTypeBand2 { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_BAND_3")]
        public string? DataTypeBand3 { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_BAND_4")]
        public string? DataTypeBand4 { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_BAND_5")]
        public string? DataTypeBand5 { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_BAND_6")]
        public string? DataTypeBand6 { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_BAND_7")]
        public string? DataTypeBand7 { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_BAND_ST_B10")]
        public string? DataTypeBandStB10 { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_THERMAL_RADIANCE")]
        public string? DataTypeThermalRadiance { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_UPWELL_RADIANCE")]
        public string? DataTypeUpwellRadiance { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_DOWNWELL_RADIANCE")]
        public string? DataTypeDownwellRadiance { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_ATMOSPHERIC_TRANSMITTANCE")]
        public string? DataTypeAtmosphericTransmittance { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_EMISSIVITY")]
        public string? DataTypeEmissivity { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_EMISSIVITY_STDEV")]
        public string? DataTypeEmissivityStdev { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_CLOUD_DISTANCE")]
        public string? DataTypeCloudDistance { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_QUALITY_L2_AEROSOL")]
        public string? DataTypeQualityL2Aerosol { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_QUALITY_L2_SURFACE_TEMPERATURE")]
        public string? DataTypeQualityL2SurfaceTemperature { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_QUALITY_L1_PIXEL")]
        public string? DataTypeQualityL1Pixel { set; get; }
        [XmlElement(ElementName = "DATA_TYPE_QUALITY_L1_RADIOMETRIC_SATURATION")]
        public string? DataTypeQualityL1RadiometricSaturation { set; get; }
    }
}
