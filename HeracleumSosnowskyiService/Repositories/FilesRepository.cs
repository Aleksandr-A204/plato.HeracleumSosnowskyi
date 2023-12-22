using HeracleumSosnowskyiService.Helpers;
using HeracleumSosnowskyiService.Interfaces;
using HeracleumSosnowskyiService.Models;
using HeracleumSosnowskyiService.MongoDb.Configuration;
using HeracleumSosnowskyiService.MongoDb.Data;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Threading;

namespace HeracleumSosnowskyiService.Storage
{
    public class FilesRepository : MongoDBContext, IFilesRepository
    {
        public FilesRepository(IOptions<MongoDbConfiguration> settings) : base(settings.Value) { }

        public IMongoCollection<FileInfoApi> FilesInfoCollection() => Database.GetCollection<FileInfoApi>("file.information");

        public IGridFSBucket GridFilesStream() => new GridFSBucket(Database);

        public async Task CreateFileInfoAsync(FileInfoApi newFileInfo, CancellationToken ct) => await FilesInfoCollection().InsertOneAsync(document: newFileInfo, cancellationToken: ct);

        public async Task<ObjectId> UploadFileStreamAsync(string filename, Stream source, CancellationToken ct) => await GridFilesStream().UploadFromStreamAsync(filename, source, cancellationToken: ct);

        public async Task<GridFSDownloadStream<ObjectId>> DouwloadFileStreamAsync(string filename)
        {
            return await GridFilesStream().OpenDownloadStreamByNameAsync(filename);
            // return await GridFilesStream().OpenDownloadStreamAsync(fileId);
        }

        public Task<bool> DeleteFile(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FileInfoApi>> GetAllAsync() => await FilesInfoCollection().Find(_ => true).ToListAsync();

        public async Task<FileInfoApi> GetFileInfoByIdAsync(string id, CancellationToken ct) => await FilesInfoCollection().Find(field => field.Id == id).FirstOrDefaultAsync(ct);

        public async Task UpdateFileInfoAsync(string id, ObjectId fsId) => await FilesInfoCollection().UpdateOneAsync(f => f.Id == id,
                Builders<FileInfoApi>.Update.Set(fs => fs.FileStreamId, fsId));
    }
}
