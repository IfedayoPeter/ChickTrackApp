

namespace ChickTrack.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CoreDbContext>();
            services.AddScoped<IApplicationDbContext, CoreDbContext>();
            return services;
        }

    }
}
