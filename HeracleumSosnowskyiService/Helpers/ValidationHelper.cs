using MongoDB.Bson;

namespace HeracleumSosnowskyiService.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsUlidValid(string? id)
        {
            return Ulid.TryParse(id, out _);
        }

        public static bool IsBsonIdValid(string? id)
        {
            return ObjectId.TryParse(id, out _);
        }
    }
}
