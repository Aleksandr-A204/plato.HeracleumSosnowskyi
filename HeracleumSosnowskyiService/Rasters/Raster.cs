using OSGeo.GDAL;
using OSGeo.OSR;

namespace HeracleumSosnowskyiService.RasterInfo
{
    internal class Raster
    {
        private readonly Stream _source;

        public Raster()
        {
            //_source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public object Info(byte[] buffer)
        {
            string memFilename = "inmemfile.tiff";

                /* -------------------------------------------------------------------- */
                /*      Open dataset.                                                   */
                /* -------------------------------------------------------------------- */
                Gdal.FileFromMemBuffer(memFilename, buffer);
                Dataset ds = Gdal.Open(memFilename, Access.GA_ReadOnly);

                Console.WriteLine("Raster dataset parameters:");
                Console.WriteLine("  Projection: " + ds.GetProjectionRef());
                Console.WriteLine("  RasterCount: " + ds.RasterCount);
                Console.WriteLine("  RasterSize (" + ds.RasterXSize + "," + ds.RasterYSize + ")");

                /* -------------------------------------------------------------------- */
                /*      Get driver                                                      */
                /* -------------------------------------------------------------------- */
                Driver drv = ds.GetDriver();

                if (drv == null)
                {
                    Console.WriteLine("Can't get driver.");
                    System.Environment.Exit(-1);
                }

                Console.WriteLine("Using driver " + drv.LongName);

                /* -------------------------------------------------------------------- */
                /*      Get metadata                                                    */
                /* -------------------------------------------------------------------- */
                string[] metadata = ds.GetMetadata("");
                if (metadata.Length > 0)
                {
                    Console.WriteLine("  Metadata:");
                    for (int iMeta = 0; iMeta < metadata.Length; iMeta++)
                    {
                        Console.WriteLine("    " + iMeta + ":  " + metadata[iMeta]);
                    }
                    Console.WriteLine("");
                }

                /* -------------------------------------------------------------------- */
                /*      Report "IMAGE_STRUCTURE" metadata.                              */
                /* -------------------------------------------------------------------- */
                metadata = ds.GetMetadata("IMAGE_STRUCTURE");
                if (metadata.Length > 0)
                {
                    Console.WriteLine("  Image Structure Metadata:");
                    for (int iMeta = 0; iMeta < metadata.Length; iMeta++)
                    {
                        Console.WriteLine("    " + iMeta + ":  " + metadata[iMeta]);
                    }
                    Console.WriteLine("");
                }

                /* -------------------------------------------------------------------- */
                /*      Report subdatasets.                                             */
                /* -------------------------------------------------------------------- */
                metadata = ds.GetMetadata("SUBDATASETS");
                if (metadata.Length > 0)
                {
                    Console.WriteLine("  Subdatasets:");
                    for (int iMeta = 0; iMeta < metadata.Length; iMeta++)
                    {
                        Console.WriteLine("    " + iMeta + ":  " + metadata[iMeta]);
                    }
                    Console.WriteLine("");
                }

                /* -------------------------------------------------------------------- */
                /*      Report geolocation.                                             */
                /* -------------------------------------------------------------------- */
                metadata = ds.GetMetadata("GEOLOCATION");
                if (metadata.Length > 0)
                {
                    Console.WriteLine("  Geolocation:");
                    for (int iMeta = 0; iMeta < metadata.Length; iMeta++)
                    {
                        Console.WriteLine("    " + iMeta + ":  " + metadata[iMeta]);
                    }
                    Console.WriteLine("");
                }

                /* -------------------------------------------------------------------- */
                /*      Report corners.                                                 */
                /* -------------------------------------------------------------------- */
                Console.WriteLine("Corner Coordinates:");
                Console.WriteLine("  Upper Left (" + GDALInfoGetPosition(ds, 0.0, 0.0) + ")");
                Console.WriteLine("  Lower Left (" + GDALInfoGetPosition(ds, 0.0, ds.RasterYSize) + ")");
                Console.WriteLine("  Upper Right (" + GDALInfoGetPosition(ds, ds.RasterXSize, 0.0) + ")");
                Console.WriteLine("  Lower Right (" + GDALInfoGetPosition(ds, ds.RasterXSize, ds.RasterYSize) + ")");
                Console.WriteLine("  Center (" + GDALInfoGetPosition(ds, ds.RasterXSize / 2, ds.RasterYSize / 2) + ")");
                Console.WriteLine("");

                /* -------------------------------------------------------------------- */
                /*      Report projection.                                              */
                /* -------------------------------------------------------------------- */
                string projection = ds.GetProjectionRef();
                if (projection != null)
                {
                    SpatialReference srs = new SpatialReference(null);
                    if (srs.ImportFromWkt(ref projection) == 0)
                    {
                        string wkt;
                        srs.ExportToPrettyWkt(out wkt, 0);
                        Console.WriteLine("Coordinate System is:");
                        Console.WriteLine(wkt);
                    }
                    else
                    {
                        Console.WriteLine("Coordinate System is:");
                        Console.WriteLine(projection);
                    }
                }

                /* -------------------------------------------------------------------- */
                /*      Report GCPs.                                                    */
                /* -------------------------------------------------------------------- */
                if (ds.GetGCPCount() > 0)
                {
                    Console.WriteLine("GCP Projection: ", ds.GetGCPProjection());
                    GCP[] GCPs = ds.GetGCPs();
                    for (int i = 0; i < ds.GetGCPCount(); i++)
                    {
                        Console.WriteLine("GCP[" + i + "]: Id=" + GCPs[i].Id + ", Info=" + GCPs[i].Info);
                        Console.WriteLine("          (" + GCPs[i].GCPPixel + "," + GCPs[i].GCPLine + ") -> ("
                                    + GCPs[i].GCPX + "," + GCPs[i].GCPY + "," + GCPs[i].GCPZ + ")");
                        Console.WriteLine("");
                    }
                    Console.WriteLine("");

                    double[] transform = new double[6];
                    Gdal.GCPsToGeoTransform(GCPs, transform, 0);
                    Console.WriteLine("GCP Equivalent geotransformation parameters: ", ds.GetGCPProjection());
                    for (int i = 0; i < 6; i++)
                        Console.WriteLine("t[" + i + "] = " + transform[i].ToString());
                    Console.WriteLine("");
                }
                Gdal.Unlink(memFilename);

            return new object();
        }

        private static string GDALInfoGetPosition(Dataset ds, double x, double y)
        {
            double[] adfGeoTransform = new double[6];
            double dfGeoX, dfGeoY;
            ds.GetGeoTransform(adfGeoTransform);

            dfGeoX = adfGeoTransform[0] + adfGeoTransform[1] * x + adfGeoTransform[2] * y;
            dfGeoY = adfGeoTransform[3] + adfGeoTransform[4] * x + adfGeoTransform[5] * y;

            return dfGeoX.ToString() + ", " + dfGeoY.ToString();
        }

        private byte[] ConvertToBinary()
        {
            using Stream stream = File.Open("test.tif", FileMode.Create);

            var _fileStream = (FileStream)stream;

            byte[] imageBuffer = new byte[2];

            using (var binaryReader = new BinaryReader(_fileStream))
            {
                int numBytes = (int)_fileStream.Length;
                imageBuffer = binaryReader.ReadBytes(numBytes);
                binaryReader.Close();
                _fileStream.Close();
            }

            return imageBuffer;
        }
    }
}
