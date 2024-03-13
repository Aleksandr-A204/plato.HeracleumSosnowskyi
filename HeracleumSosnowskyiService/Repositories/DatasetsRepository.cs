using HeracleumSosnowskyiService.Data.MongoDb;
using HeracleumSosnowskyiService.Data.PostgreSQL;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.MongoDb.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace HeracleumSosnowskyiService.Repositories
{
    public class DatasetsRepository : MongoDBContext, IDatasetsRepository
    {
        private readonly PostgreSQLDbContext _context;

        public DatasetsRepository(PostgreSQLDbContext context, IOptions<MongoDbConfiguration> settings) : base(settings.Value)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGridFSBucket GridFilesStream() => new GridFSBucket(Database);

        public async Task<IEnumerable<SatelliteDataOfSpacesystem>> GetSatellitesDataAsync() => await _context.SatelliteData.ToListAsync();

        public async Task<IEnumerable<Dataset>> GetDatasetsBySatelliteDataIdAsync(Ulid id) =>
            await _context.Datasets.Where(field => field.SatelliteDataId == id).ToArrayAsync();

        public async Task<SatelliteDataOfSpacesystem> GetByIdAsync(Ulid id) =>
            await _context.SatelliteData.Include(field => field.Datasets).FirstOrDefaultAsync(s => s.Id == id) ?? new();

        public async Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(ObjectId fileId) 
            => await GridFilesStream().OpenDownloadStreamAsync(fileId);

        //public async Task<FileInfoApi> GetFileInfoByIdAsync(Ulid id) =>
        //    await _context.FileInfo.Include(x => x.Datasets).FirstOrDefaultAsync(field => field.Id == id) ?? new FileInfoApi();

        public async Task<byte[]> DownloadAsBytesAsync(ObjectId id) => await GridFilesStream().DownloadAsBytesAsync(id);

    }
}
