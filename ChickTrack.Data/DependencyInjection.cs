using Lagetronix.Rapha.Base.Common.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChickTrack.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {        
            services.AddScoped<IApplicationDbContext, CoreDbContext>();
            return services;
        }

    }
}
