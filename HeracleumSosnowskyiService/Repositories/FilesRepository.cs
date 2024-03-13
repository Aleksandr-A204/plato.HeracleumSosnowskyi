using HeracleumSosnowskyiService.Data.MongoDb;
using HeracleumSosnowskyiService.Data.PostgreSQL;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.MongoDb.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;

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

        public async Task<IEnumerable<FileInfoApi>> GetAllFileInfoAsync() => await _context.FileInfo.ToListAsync();

        public async Task<bool> TryAddAsync(FileInfoApi fileInfo)
        {
            await _context.FileInfo.AddAsync(fileInfo);
            return await SaveAsync();
        }

        public async Task<FileInfoApi> GetFileInfoByIdAsync(Ulid id) => 
            await _context.FileInfo.Include(x => x.Datasets).FirstOrDefaultAsync(field => field.Id == id) ?? new FileInfoApi();
        public async Task<FileInfoApi> GetFileInfoByIdAsync(string id) =>
            await _context.FileInfo.Include(x => x.Datasets).FirstOrDefaultAsync(field => field.FileStreamId == id) ?? new FileInfoApi();

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> TryUpdateAsync(FileInfoApi fileInfo)
        {
            _context.Entry(fileInfo).State = EntityState.Modified;
            return await SaveAsync();
        }

        public async Task<SatelliteDataOfSpacesystem> FindOrInsertAsync(string landsatProductId) 
            => await _context.SatelliteData.FirstOrDefaultAsync(field 
                => field.LandsatProductId == landsatProductId) ?? new SatelliteDataOfSpacesystem { LandsatProductId = landsatProductId };

        public async Task<string> UploadFileStreamAsync(string filename, Stream source)
        {
            var fileInfo = await FindByFilenameAsync(filename);

            if (fileInfo != null)
                await GridFilesStream().DeleteAsync(fileInfo.Id);

            return (await GridFilesStream().UploadFromStreamAsync(filename, source)).ToString();
        }

        private async Task<GridFSFileInfo> FindByFilenameAsync(string filename)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Filename, filename);
            var fileInfos = await GridFilesStream().FindAsync(filter);

            return fileInfos.FirstOrDefault();
        }

        public async Task<byte[]> DownloadAsBytesAsync(ObjectId id) => await GridFilesStream().DownloadAsBytesAsync(id);

        public async Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(ObjectId id) 
            => await GridFilesStream().OpenDownloadStreamAsync(id);        
    }
}
