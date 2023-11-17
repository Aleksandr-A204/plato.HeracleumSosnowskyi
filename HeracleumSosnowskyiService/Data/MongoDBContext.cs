using HeracleumSosnowskyiService.Models;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(string? connectionString, string? databaseName)
        {
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<FileApi> FilesInfo => 
            _database.GetCollection<FileApi>("FilesInforamion");

        public IGridFSBucket GridFilesStream =>
            new GridFSBucket(_database);
    }
}
