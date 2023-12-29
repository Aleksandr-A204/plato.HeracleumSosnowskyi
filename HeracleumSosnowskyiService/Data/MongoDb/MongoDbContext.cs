using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.MongoDb.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HeracleumSosnowskyiService.Data.MongoDb
{
    public class MongoDBContext
    {
        protected IMongoDatabase? Database { get; }

        public MongoDBContext(MongoDbConfiguration settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings), "Конфигурация MongoDb(MongoDbConfiguration) не может иметь значение NULL.");

            if (settings.ConnectionString == null)
                throw new ArgumentNullException(nameof(settings), "Строка подключения конфигурации MongoDb(MongoDbConfiguration.ConnectionString) не может иметь значение NULL.");

            var mongoClient = new MongoClient(settings.ConnectionString);

            if (settings.DatabaseName == null && mongoClient == null)
                throw new ArgumentNullException(nameof(settings), "Имя базы данных(MongoDBConfiguration.Database) не может иметь значение NULL.");

            Database = mongoClient.GetDatabase(settings.DatabaseName);

            if (Database == null)
                throw new ArgumentNullException(nameof(Database), "База данных(IMongoDatabase.Database) не может иметь значение NULL.");
        }

        //protected void Initialize()
        //{
        //    if (Database == null)
        //        throw new ArgumentNullException(nameof(Database), "База данных(IMongoDatabase.Database) не может иметь значение NULL.");

        //    var datasets = Database.GetCollection<SatelliteImageDataset>("satellite.images.dataset");

        //    var keys = Builders<SatelliteImageDataset>.IndexKeys.Ascending(field => field.LandsatProductId);

        //    datasets.Indexes.CreateOne(new CreateIndexModel<SatelliteImageDataset>(keys, new CreateIndexOptions { Unique = true }));
        //}
    }
}
