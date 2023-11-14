using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;


namespace HeracleumSosnowskyiService.Storage
{
    public class FilesRepository : IFilesRepository
    {
        private readonly IConfiguration _configuration;

        public FilesRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task<string> AddFile(Stream fileStream, CancellationToken canellationToken)
        {
            var connectionString = _configuration.GetSection("Storage:ConnectingToMongoDB").Value;
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("SatelliteImages");

            IGridFSBucket gridFS = new GridFSBucket(db);



            var fs = File.OpenRead($"{Directory.GetCurrentDirectory()}/upload/LC08_L2SP_178022_20220522_20220525_02_T1_SR_B3.TIF");

            ObjectId fileId = await gridFS.UploadFromStreamAsync("LC08_L2SP_178022_20220522_20220525_02_T1_SR_B3.TIF", fs);

            return "test".ToString();
        }
        public async Task<Stream> GetFile(HttpResponse response, string id, CancellationToken canellationToken)
        {
            var connectionString = _configuration.GetSection("Storage:ConnectingToMongoDB").Value;
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("SatelliteImages");

            IGridFSBucket gridFS = new GridFSBucket(db);

            var fileStream = await gridFS.OpenDownloadStreamAsync(new ObjectId(id));

            return fileStream;
        }
    }
}
