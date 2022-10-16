namespace lcpsnapi.Extensions
{
    public static class SizeUnits
    {
        public enum ESizeUnits
        {
            Bit = 0, 
            Byte = 1, 
            KB = 2, 
            MB = 3, 
            GB = 4, 
            TB = 5
        }

        // https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc or https://stackoverflow.com/a/14489026
        public static dynamic GetValSizeUnit(ESizeUnits unit, dynamic rv, int decPlaces = 2)
        {
            return unit switch
            {
                ESizeUnits.Bit => Math.Round((double)((Humanizer.Bytes.ByteSize)rv).Bits, decPlaces) + " Bits",
                ESizeUnits.Byte => Math.Round((double)((Humanizer.Bytes.ByteSize)rv).Bytes, decPlaces) + " Bytes",
                ESizeUnits.KB => Math.Round((double)((Humanizer.Bytes.ByteSize)rv).Kilobytes, decPlaces) + " KB",
                ESizeUnits.MB => Math.Round((double)((Humanizer.Bytes.ByteSize)rv).Megabytes, decPlaces) + " MB",
                ESizeUnits.GB => Math.Round((double)((Humanizer.Bytes.ByteSize)rv).Gigabytes, decPlaces) + " GB",
                ESizeUnits.TB => Math.Round((double)((Humanizer.Bytes.ByteSize)rv).Terabytes, decPlaces) + " TB",
                _ => Math.Round((double)((Humanizer.Bytes.ByteSize)rv).Bits, decPlaces) + " Bits"
            };
        }
    }
}
