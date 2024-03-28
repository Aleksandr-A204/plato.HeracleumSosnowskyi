using HeracleumSosnowskyiService.Data.MongoDb;
using HeracleumSosnowskyiService.Data.PostgreSQL;
using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.MongoDb.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace HeracleumSosnowskyiService.Repositories
{
    public class DatasetsRepository : MongoDBContext, IDatasetsRepository
    {
        private readonly PostgreSQLDbContext _context;

        public DatasetsRepository(PostgreSQLDbContext context, IOptions<MongoDbConfiguration> settings) : base(settings.Value)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGridFSBucket GridFilesStream() 
            => new GridFSBucket(Database);

        public async Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(ObjectId fileId) 
            => await GridFilesStream().OpenDownloadStreamAsync(fileId);

        public async Task<byte[]> DownloadAsBytesAsync(ObjectId id) 
            => await GridFilesStream().DownloadAsBytesAsync(id);

        public async Task<bool> TryAddMetadataAsync(LandsatMetadata metadata)
        {
            await _context.AddAsync(metadata);
            return await SaveAsync();
        }

        private async Task<bool> SaveAsync() 
            => await _context.SaveChangesAsync() > 0;

        public async Task<bool> TryAddAsync(Dataset metadata)
        {
            await _context.Datasets.AddAsync(metadata);
            return await SaveAsync();
        }

        public async Task<FileInfoApi> GetInfoFileAsync(string id) 
            => await _context.FileInfo.FirstOrDefaultAsync(field => field.FileStreamId == id) ?? new FileInfoApi();

        public async Task<LandsatMetadata> GetMetadataAsync(string id)
            => await _context.LandsatMetadata.FindAsync(id) ?? new LandsatMetadata();

        public async Task<bool> TryUpdateAsync(string id, Ulid ulid)
        {
            var fileInfo = await GetInfoFileAsync(id);

            await _context.Datasets.AddAsync(new Dataset { FileStreamId = id, LandsatMetadataId = ulid, FileInfoId = fileInfo.Id });
            return await SaveAsync();
        }
    }
}
