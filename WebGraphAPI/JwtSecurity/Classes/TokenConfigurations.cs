namespace JwtSecurity.Classes
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public int SecondsRefresh { get; set; }
        public string SecretKey { get; set; }
    }
}
