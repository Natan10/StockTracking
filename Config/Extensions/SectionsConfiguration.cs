namespace StockTracking.Config.Extensions
{
    public static class SectionsConfiguration
    {
        public static void AddSectionsExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        }
    }
}
