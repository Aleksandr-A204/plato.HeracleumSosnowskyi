using MongoDB.Bson;
using System.Numerics;

namespace HeracleumSosnowskyiService.Models
{
    public class File
    {
        public ObjectId Id { get; set; } = ObjectId.Empty;
        public string? Name { get; set; }
        public BigInteger Size { get; set; }
        public string? Type { get; set; }
        public string? LastModifiedDate { get; set; }

    }
}
