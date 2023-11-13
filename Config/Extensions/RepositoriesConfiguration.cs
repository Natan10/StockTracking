using StockTracking.Repositories;

namespace StockTracking.Config.Extensions
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositoriesExtension(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<ISolicitationRepository, SolicitationRepository>();
        }
    }
}
