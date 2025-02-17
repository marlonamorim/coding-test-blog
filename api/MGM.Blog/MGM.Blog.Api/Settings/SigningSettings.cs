using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace MGM.Blog.Api.Settings
{
    public class SigningSettings
    {
        public SecurityKey? Key { get; }

        public SigningCredentials? SigningCredentials { get; }

        public SigningSettings()
        {
            using var provider = new RSACryptoServiceProvider(2048);
            Key = new RsaSecurityKey(provider.ExportParameters(true));

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
