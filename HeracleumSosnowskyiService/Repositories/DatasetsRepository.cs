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
    public class DatasetsRepository : MongoDBContext, IDatasetsRepository
    {
        private readonly PostgreSQLDbContext _context;

        public DatasetsRepository(PostgreSQLDbContext context, IOptions<MongoDbConfiguration> settings) : base(settings.Value)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGridFSBucket GridFilesStream() => new GridFSBucket(Database);

        public async Task<IEnumerable<Dataset>> GetAllAsync() => await _context.Datasets.ToListAsync();

        public async Task<Dataset> GetByIdAsync(Ulid id) =>
            await _context.Datasets.FirstOrDefaultAsync(s => s.Id == id) ?? new();

        public async Task<GridFSDownloadStream<ObjectId>> DouwloadFileStreamAsync(ObjectId fileId) => await GridFilesStream().OpenDownloadStreamAsync(fileId);

    }
}
