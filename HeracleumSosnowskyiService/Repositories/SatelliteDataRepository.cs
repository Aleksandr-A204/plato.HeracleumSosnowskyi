using HeracleumSosnowskyiService.Data.MongoDb;
using HeracleumSosnowskyiService.Data.PostgreSQL;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.MongoDb.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.Data;

namespace HeracleumSosnowskyiService.Repositories
{
    public class SatelliteDataRepository : MongoDBContext, ISatelliteDataRepository
    {
        private readonly PostgreSQLDbContext _context;

        public SatelliteDataRepository(PostgreSQLDbContext context, IOptions<MongoDbConfiguration> settings) : base(settings.Value)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGridFSBucket GridFilesStream() => new GridFSBucket(Database);

        public async Task<IEnumerable<SatelliteDataOfSpacesystem>> GetAllAsync() => await _context.SatelliteData.ToListAsync();

        public async Task<SatelliteDataOfSpacesystem> GetByIdAsync(Ulid id) => await _context.SatelliteData.Include(field => field.Datasets).FirstOrDefaultAsync(s => s.Id == id) ?? new();

        public async Task<GridFSDownloadStream<ObjectId>> DouwloadFileStreamAsync(ObjectId fileId) => await GridFilesStream().OpenDownloadStreamAsync(fileId);
    }
}
