using AutoMapper;
using ChickTrack.Service.Implementations;
using ChickTrack.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChickTrack.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IInvestmentService, InvestmentService>();
            services.AddScoped<IExpensesService, ExpensesService>();
            services.AddScoped<IPoultryService, PoultryService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperConfig>();
            });

            // Register IMapper
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddAutoMapper(typeof(DependencyInjection));
            services.AddControllersWithViews();

            services.AddHttpClient();
            return services;
        }
    }
}
