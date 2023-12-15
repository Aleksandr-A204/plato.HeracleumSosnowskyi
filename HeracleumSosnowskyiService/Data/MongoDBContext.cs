using HeracleumSosnowskyiService.Configuration;
using HeracleumSosnowskyiService.Models;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Data
{
    public class MongoDBContext
    {
        private IMongoDatabase? Database { get; }

        public MongoDBContext(MongoDbSettings mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
            Database = mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
        }

        public IMongoCollection<FileInfoApi> FilesInfoCollection() =>
            Database.GetCollection<FileInfoApi>("FilesInformation");

        public IGridFSBucket GridFilesStream() =>
            new GridFSBucket(Database);
    }
}
