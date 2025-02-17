namespace MGM.Blog.Api.Settings
{
    public class TokenSettings
    {
        public string Audience { get; set; } = null!;

        public string Issuer { get; set; } = null!;

        public int Seconds { get; set; }
    }
}
