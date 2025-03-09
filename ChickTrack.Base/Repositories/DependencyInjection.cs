using ChickTrack.Base.Domain.Entities;
using ChickTrack.Base.Repositories.Implementations;
using ChickTrack.Base.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddBaseRepositoryDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Register ApplicationDbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddHttpContextAccessor();

        // Register IApplicationDbContext
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        // Identity services
        services.AddIdentity<BaseUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}