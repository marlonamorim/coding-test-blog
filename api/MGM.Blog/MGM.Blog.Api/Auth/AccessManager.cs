using MGM.Blog.Api.Settings;
using MGM.Blog.AppServices.ViewModel;
using MGM.Blog.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MGM.Blog.Api.Auth
{
    public class AccessManager(SigningSettings signingSettings, 
        TokenSettings tokenSettings,
        IUserRepository userRepository) : IAccessManager
    {
        private readonly SigningSettings _signingSettings = signingSettings;
        private readonly TokenSettings _tokenSettings = tokenSettings;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> CredentialsIsValid(CredentialViewModel credential)
            => await _userRepository.CredentialIsValidAsync(credential.Email, credential.Password);

        public async Task<LoginViewModel> GenerateTokenAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            
            ClaimsIdentity identity = new(
                    [
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.Email, email),
                        new Claim(ClaimTypes.NameIdentifier, user!.Id.ToString()!)
                    ]
                );

            var currentDate = DateTime.Now;
            var expirationDate = currentDate + TimeSpan.FromSeconds(_tokenSettings.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor { 
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                SigningCredentials = _signingSettings.SigningCredentials,
                Subject = identity,
                NotBefore = currentDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return new LoginViewModel { 
                Authenticated = true,
                CreateAt = currentDate,
                Expiration = expirationDate,
                AccessToken = token
            };
        }
    }
}
