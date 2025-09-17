

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
            services.AddScoped<IFeedUnitPriceService, FeedUnitPriceService>();
            services.AddScoped<IFeedPriceService, FeedPriceService>();
            services.AddScoped<FeedProfitCalculator>();

            //Financials
            services.AddScoped<IExpensesService, ExpensesService>();
            services.AddScoped<IInvestmentService, InvestmentService>();
            services.AddScoped<ISaleRecordService, SalesRecordService>();
            services.AddScoped<ITotalSalesService, TotalSalesService>();

            //Poultry
            services.AddScoped<IBirdService, BirdService>();
            services.AddScoped<IBirdTransactionService, BirdTransactionService>();
            services.AddScoped<IEggInventoryService, EggInventoryService>(); ;
            services.AddScoped<IEggTransactionService, EggTransactionService>();


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
