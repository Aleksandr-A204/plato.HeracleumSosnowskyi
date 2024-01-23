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
    public class FilesRepository : MongoDBContext, IFilesRepository
    {
        private readonly PostgreSQLDbContext _context;

        public FilesRepository(PostgreSQLDbContext context, IOptions<MongoDbConfiguration> settings) : base(settings.Value)
        {
            _context = context;
        }

        public IGridFSBucket GridFilesStream() => new GridFSBucket(Database);

        //public async Task<IEnumerable<Dataset>> GetAllAsync()
        //    => await _context.Datasets.Include(x => x.FileInfo).Include(x => x.SatelliteData).ToListAsync();

        public async Task<IEnumerable<FileInfoApi>> GetAllFileInfoAsync() => await _context.FileInfo.ToListAsync();

        public async Task<bool> TryAddAsync(Dataset datasets)
        {
            await _context.Datasets.AddAsync(datasets);
            return await SaveAsync();
        }

        public async Task<FileInfoApi> GetFileInfoByIdAsync(Ulid id) => 
            await _context.FileInfo.Include(x => x.Datasets).FirstOrDefaultAsync(field => field.Id == id) ?? new FileInfoApi();

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> TryUpdateAsync(Dataset datasets)
        {
            _context.Entry(datasets).State = EntityState.Modified;
            return await SaveAsync();
        }

        public async Task<SatelliteDataOfSpacesystem> FindOrInsertAsync(string landsatProductId) 
            => await _context.SatelliteData.FirstOrDefaultAsync(field 
                => field.LandsatProductId == landsatProductId) ?? new SatelliteDataOfSpacesystem { LandsatProductId = landsatProductId };

        public async Task<string> UploadFileStreamAsync(string filename, Stream source) => (await GridFilesStream().UploadFromStreamAsync(filename, source)).ToString();
    }
}
