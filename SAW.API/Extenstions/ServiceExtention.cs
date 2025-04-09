using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SAW.API.Extentions
{
    public static class ServiceExtention
    {
        public static void ConfigureAuthenticationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSecret = configuration.GetSection("AppSettings").GetValue<string>("JwtKey");
            var frontUrl = configuration.GetSection("AppSettings").GetValue<string>("FrontUrl");
            var key = Encoding.ASCII.GetBytes(jwtSecret);

            services.AddAuthentication(
                x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = frontUrl, //some string, normally web url,
                        ValidAudience = frontUrl,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

        }
    }
}
