using MongoDB.Bson;

namespace HeracleumSosnowskyiService.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsIdValid(string? id)
        {
            return Ulid.TryParse(id, out _);
        }
    }
}
