namespace MGM.Blog.AppServices.ViewModel
{
    public class LoginViewModel
    {
        public bool Authenticated { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime Expiration { get; set; }

        public string AccessToken { get; set; } = null!;
    }
}
