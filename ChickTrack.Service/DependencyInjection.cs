using AutoMapper;
using ChickTrack.Service.Implementations.Feed;
using ChickTrack.Service.Implementations.Financial;
using ChickTrack.Service.Implementations.Poultry;
using ChickTrack.Service.Interfaces.Feed;
using ChickTrack.Service.Interfaces.Financial;
using ChickTrack.Service.Interfaces.Poultry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChickTrack.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Feed
            services.AddScoped<IFeedInventoryService, FeedInventoryService>();
            services.AddScoped<IFeedLogService, FeedLogService>();
            services.AddScoped<IFeedSalesUnitService, FeedSalesUnitService>();

            //Financials
            services.AddScoped<IExpensesService, ExpensesService>();
            services.AddScoped<IInvestmentService, InvestmentService>();
            services.AddScoped<ISaleRecordService, SalesRecordService>();
            services.AddScoped<ITotalSalesService, TotalSalesService>();

            //Poultry
            services.AddScoped<IBirdService, BirdService>();
            services.AddScoped<IBirdTransactionService, BirdTransactionService>();
            services.AddScoped<IBirdTransactionService, BirdTransactionService>();
            services.AddScoped<IEggInventoryService, EggInventoryService>();
            services.AddScoped<IEggTransactionService, EggTransactionService>();
            services.AddScoped<IEggTransactionService, EggTransactionService>();
            services.AddScoped<IEggInventoryService, EggInventoryService>();


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
