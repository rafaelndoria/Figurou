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
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IPaginaAlbumRepository, PaginaAlbumRepository>();
            services.AddScoped<IFigurinhaRepository, FigurinhaRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IArquivoService, ArquivoService>();
            services.AddScoped<IPaginaAlbumService, PaginaAlbumService>();
            services.AddScoped<IFigurinhaService, FigurinhaService>();

            return services;
        }
    }
}
