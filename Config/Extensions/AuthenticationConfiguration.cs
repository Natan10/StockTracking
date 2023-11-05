using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StockTracking.Config.Extensions
{
    public static class AuthenticationConfiguration
    {
       

        public static void AddAuthenticationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            var key = Encoding.UTF8.GetBytes(jwtOptions.SecurityKey);

            AddJwtConfiguration(services, key);
        }

        private static void AddJwtConfiguration(IServiceCollection services, byte[] key)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,

                       IssuerSigningKey = new SymmetricSecurityKey(key)
                   };
               });
        }

    }
}
