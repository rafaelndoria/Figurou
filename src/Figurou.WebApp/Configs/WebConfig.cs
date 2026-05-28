using Figurou.Data.Context;

using Microsoft.EntityFrameworkCore;

namespace Figurou.WebApp.Configs
{
    public static class WebConfig
    {
        public static IServiceCollection AddWebAppConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddJwtConfig(configuration);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddInjecaoDependenciaConfig();

            return services;
        }
    }
}
