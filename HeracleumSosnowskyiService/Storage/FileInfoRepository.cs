using HeracleumSosnowskyiService.Data;
using HeracleumSosnowskyiService.Models;
using MongoDB.Driver;

namespace HeracleumSosnowskyiService.Storage
{
    public class FileInfoRepository
    {
        private readonly MongoDBContext _context;

        public FileInfoRepository(MongoDBContext context)
        {
            _context = context;
        }

        private struct Entry
        {
            public Entry(Dictionary<string, string> value)
            {
                uploadDate = DateTime.Now;
                Value = value;
            }

            public DateTime uploadDate { get; set; }
            public Dictionary<string, string> Value { get; set; }
        }
        public async Task<List<FileApi>> GetAllAsync() =>
            await _context.FilesInfo.Find(_ => true).ToListAsync();
        public async Task CreateAsync(FileApi newFile) =>
            await _context.FilesInfo.InsertOneAsync(newFile);
    }
}
