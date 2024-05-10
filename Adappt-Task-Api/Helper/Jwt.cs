namespace Api.Helper
{
    public class Jwt
    {
        public string Key { get; set; } = string.Empty;

        public int ExpiryTimeInMinutes { get; set; }

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;
    }
}
