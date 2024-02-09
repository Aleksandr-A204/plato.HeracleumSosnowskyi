using System.Runtime.Serialization;

namespace HeracleumSosnowskyiService.USGS.EROS.API
{
    [DataContract]
    public class LoginToken
    {
        [DataMember(Name = "username")]
        public string Username { get; } = "ponomarev-204@mail.ru";

        [DataMember(Name = "token")]
        public string Token { get; } = "FAJVB6vx8D1uStV2AuRdQftyA2AinFC_S0O5F9CAy8bQ7TLssE@H1JZl8triIkI!";
    }
}
