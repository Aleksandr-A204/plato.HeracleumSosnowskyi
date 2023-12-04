using HeracleumSosnowskyiService.Configuration;
using HeracleumSosnowskyiService.Data;
using HeracleumSosnowskyiService.Interfaces;
using HeracleumSosnowskyiService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HeracleumSosnowskyiService.Storage
{
    public class FilesRepository : MongoDBContext, IFilesRepository
    {
        public FilesRepository(IServiceProvider serviceProvider) : base(serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value)
        {
        }

        public async Task CreateFileInfoAsync(FileInfoApi newFileInfo) => await FilesInfoCollection.InsertOneAsync(newFileInfo);

        public async Task<ObjectId> CreateFileStreamAsync(string filename, Stream source) => await GridFilesStream.UploadFromStreamAsync(filename, source);

        public Task<bool> DeleteFile(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<FileInfoApi> GetFileInfoByIdAsync(string id) => await FilesInfoCollection.Find(field => field.Id == id).FirstOrDefaultAsync();

        public async Task UpdateFileInfoAsync(string id, ObjectId fsId) => await FilesInfoCollection.UpdateOneAsync(f => f.Id == id,
                Builders<FileInfoApi>.Update.Set(fs => fs.FileStreamId, fsId));
    }
}
