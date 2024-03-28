using HeracleumSosnowskyiService.Data.MongoDb;
using HeracleumSosnowskyiService.Data.PostgreSQL;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.MongoDb.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
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

        public async Task<bool> TryAddAsync(FileInfoApi fileInfo, CancellationToken ct)
        {
            await _context.FileInfo.AddAsync(fileInfo, ct);
            return await SaveAsync(ct);
        }

        public async Task<FileInfoApi> GetFileInfoByIdAsync(Ulid id, CancellationToken ct) => 
            await _context.FileInfo.FirstOrDefaultAsync(field => field.Id == id, ct) ?? new FileInfoApi();

        private async Task<bool> SaveAsync(CancellationToken ct) 
            => await _context.SaveChangesAsync(ct) > 0;

        public async Task<bool> TryUpdateAsync(FileInfoApi fileInfo, CancellationToken ct)
        {
            _context.Entry(fileInfo).State = EntityState.Modified;
            return await SaveAsync(ct);
        }

        public async Task<string> UploadOrUpdateFileStreamAsync(string id, string filename, Stream source, CancellationToken ct)
        {
            var fileInfo = await FindByFilenameAsync(filename);

            if (fileInfo != null)
            {
                var valueId = fileInfo.Metadata.ToBsonDocument().GetValue("fileInfoId").AsString;
                var f = await GetFileInfoByIdAsync(Ulid.Parse(valueId), ct);
                _context.Entry(f).State = EntityState.Deleted;
                await GridFilesStream().DeleteAsync(fileInfo.Id, ct);
            }

            return (await GridFilesStream().UploadFromStreamAsync(filename, source, new GridFSUploadOptions { Metadata = new BsonDocument("fileInfoId", id) })).ToString();
        }

        public async Task<byte[]> DownloadAsBytesAsync(ObjectId id) 
            => await GridFilesStream().DownloadAsBytesAsync(id);

        public async Task<GridFSDownloadStream<ObjectId>> DownloadFileStreamAsync(ObjectId id) 
            => await GridFilesStream().OpenDownloadStreamAsync(id);

        private async Task<GridFSFileInfo> FindByFilenameAsync(string filename)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Filename, filename);
            var fileInfos = await GridFilesStream().FindAsync(filter);

            return fileInfos.FirstOrDefault();
        }
    }
}
