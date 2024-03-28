namespace HeracleumSosnowskyiService.Data.Enum
{
    [Flags]
    public enum LandsatMetadataFlags : byte
    {
        FileNameBand1 = 1 << 0,
        FileNameBand2 = 1 << 1,
        FileNameBand3 = 1 << 2,
        FileNameBand4 = 1 << 3,
        FileNameBand5 = 1 << 4,
        FileNameBand6 = 1 << 5,
        FileNameBand7 = 1 << 6
    }
}
