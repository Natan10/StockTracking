using StockTracking.Services.Auth;
using StockTracking.Services.Solicitations;
using StockTracking.Services.Stock;

namespace StockTracking.Config.Extensions
{
    public static class ServicesConfiguration
    {
        public static void AddServicesExtension(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<ISolicitationService, SolicitationService>();
        }
    }
}
