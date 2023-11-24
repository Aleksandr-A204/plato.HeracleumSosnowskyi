using MongoDB.Bson;

namespace HeracleumSosnowskyiService.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsIdValid(string? id)
        {
            return ObjectId.TryParse(id, out _);
        }
    }
}
