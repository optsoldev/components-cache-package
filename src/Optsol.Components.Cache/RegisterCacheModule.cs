using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Optsol.Components.Cache.Connections;
using Optsol.Components.Cache.Services;
using Optsol.Components.Cache.Settings;

namespace Optsol.Components.Cache
{
    public static class RegisterCacheModule
    {
        public static IServiceCollection AddCacheModule(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSettings = new CacheSettings();
            configuration.GetSection(nameof(CacheSettings)).Bind(redisSettings);

            services.AddSingleton(redisSettings);

            services.AddSingleton<ICacheConnection, DefaultConnection>();

            services.AddScoped<ICacheService, CacheService>();

            return services;
        }
    }
}