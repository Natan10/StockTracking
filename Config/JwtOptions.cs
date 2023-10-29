namespace StockTracking.Config
{
    public class JwtOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        public int Expiration {  get; set; }
    }
}
