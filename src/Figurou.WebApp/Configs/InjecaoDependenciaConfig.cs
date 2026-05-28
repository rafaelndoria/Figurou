using Figurou.Business.Interfaces;
using Figurou.Business.Notificacoes;
using Figurou.Business.Services.Implementations;
using Figurou.Business.Services.Interfaces;
using Figurou.Data.Context;
using Figurou.Data.Repositories;

namespace Figurou.WebApp.Configs
{
    public static class InjecaoDependenciaConfig
    {
        public static IServiceCollection AddInjecaoDependenciaConfig(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            return services;
        }
    }
}
