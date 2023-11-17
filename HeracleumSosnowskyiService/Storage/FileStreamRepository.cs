using HeracleumSosnowskyiService.Configuration;
using HeracleumSosnowskyiService.Data;
using HeracleumSosnowskyiService.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace HeracleumSosnowskyiService.Storage
{
    public class FileStreamRepository : IFilesRepository
    {
        private readonly MongoDBContext _context;

        public FileStreamRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Stream newFileStream)
        {
            ObjectId fileId = await _context.GridFilesStream.UploadFromStreamAsync("fileTest", newFileStream);

            return ObjectId.TryParse(fileId.ToString(), out _);
        }

        public Task<bool> DeleteFile(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Stream> GetFileById(string id)
        {
            var fileStream = await _context.GridFilesStream.OpenDownloadStreamAsync(new ObjectId(id));

            return fileStream;
        }

        public Task<bool> UpdateFile(Stream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}
