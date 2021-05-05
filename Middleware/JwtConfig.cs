namespace TalktifAPI.Middleware
{
    public class JwtConfig
    {
        public string secret { get; set; }
        public string secret2 { get; set; }
        public int expirationInHours { get; set; }
        public int expirationInMonths { get; set; }
        public string Issuer { get; set; }
        public string Audiences { get; set; }            
    }
}