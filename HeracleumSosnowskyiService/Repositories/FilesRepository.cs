using HeracleumSosnowskyiService.Configuration;
using HeracleumSosnowskyiService.Data;
using HeracleumSosnowskyiService.Interfaces;
using HeracleumSosnowskyiService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Threading;

namespace HeracleumSosnowskyiService.Storage
{
    public class FilesRepository : MongoDBContext, IFilesRepository
    {
        public FilesRepository(IServiceProvider serviceProvider) : base(serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value)
        {
        }

        public async Task CreateFileInfoAsync(FileInfoApi newFileInfo, CancellationToken ct) => await FilesInfoCollection().InsertOneAsync(document: newFileInfo, cancellationToken: ct);

        public async Task<ObjectId> UploadFileStreamAsync(string filename, Stream source, CancellationToken ct) => await GridFilesStream().UploadFromStreamAsync(filename, source, cancellationToken: ct);

        public async Task DouwloadFileStreamAsync(ObjectId fileId) 
        {
            using Stream fs = File.OpenWrite(@"D:\проектQGIS(Тула)\111.TIF");
            await GridFilesStream().DownloadToStreamAsync(fileId, fs);
        }

        public Task<bool> DeleteFile(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<FileInfoApi> GetFileInfoByIdAsync(string id, CancellationToken ct) => await FilesInfoCollection().Find(field => field.Id == id).FirstOrDefaultAsync(ct);

        public async Task UpdateFileInfoAsync(string id, ObjectId fsId) => await FilesInfoCollection().UpdateOneAsync(f => f.Id == id,
                Builders<FileInfoApi>.Update.Set(fs => fs.FileStreamId, fsId));
    }
}
