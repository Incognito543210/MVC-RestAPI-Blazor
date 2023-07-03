using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Services
{
    public class JwtConfigure
    {
        public static void ConfigureJwtBearerOptions(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                IgnoreTrailingSlashWhenValidatingAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SecretKeySecretKey")),
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                RequireAudience = true,
                RequireSignedTokens = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidAudience = "Przepisy.View",
                ValidIssuer = "Przepisy.Api"
            };
            options.SaveToken = true;
        }
    }
}
