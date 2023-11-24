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

        public async Task<string> CreateFileStreamAsync(Stream newFileStream) => await GridFilesStream.UploadFromStreamAsync("fileTest", newFileStream);

        public Task<bool> DeleteFile(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<FileInfoApi> GetFileInfoById(string id)
        {
            return await FilesInfoCollection.Find(field => field.Id == id).FirstOrDefaultAsync();
        }

        public Task<bool> UpdateFile(Stream fileStream)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsIdEqualsAsync(string id)
        {
            throw new NotImplementedException();
        }

        private struct Entry
        {
            public Entry(FileInfoApi value)
            {
                UploadDate = DateTime.Now;
                Value = value;
            }

            public DateTime UploadDate { get; }
            public FileInfoApi Value { get; }
        }

        //public async Task<Stream> GetFileById(string id)
        //{
        //    var fileStream = await _gridFS.OpenDownloadStreamAsync(new ObjectId(id));

        //    return fileStream;
        //}
    }
}
