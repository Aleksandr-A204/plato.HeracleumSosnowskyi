using HeracleumSosnowskyiService.Interfaces;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.MongoDb.Configuration;
using HeracleumSosnowskyiService.MongoDb.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HeracleumSosnowskyiService.Repositories
{
    public class SatelliteImagesDatasetRepository : MongoDBContext, ISatelliteImagesDatasetRepository
    {
        public SatelliteImagesDatasetRepository(IOptions<MongoDbConfiguration> settings) : base(settings.Value)
        {
            Initialize();
        }

        public IMongoCollection<SatelliteImagesDataset> SatelliteImagesDatasetCollection() => Database.GetCollection<SatelliteImagesDataset>("satellite.images.dataset");
    }
}
