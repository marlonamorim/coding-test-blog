using MGM.Blog.AppServices.ViewModel;

namespace MGM.Blog.Api.Auth
{
    public interface IAccessManager
    {
        Task<bool> CredentialsIsValid(CredentialViewModel credential);

        Task<LoginViewModel> GenerateTokenAsync(string email);
    }
}