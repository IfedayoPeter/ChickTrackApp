using ChickTrack.Domain.DataTransferObjects.Feed;
using ChickTrack.Domain.DataTransferObjects.Financial;
using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Domain.Entities.Financials;
using ChickTrack.Domain.Entities.Poultry;
using Lagetronix.Rapha.Base.Common.Services;

namespace ChickTrack.Service
{
    public class AutoMapperConfig : MapperConfig
    {
        public AutoMapperConfig()
        {

        }

        protected override void ConfigureCustomMappings()
        {
            //Feed
            CreateMap<FeedInventory, FeedInventoryDto>()
                .ReverseMap();
            CreateMap<FeedLog, FeedLogDto>()
                .ReverseMap();
            CreateMap<FeedSalesUnit, FeedSalesUnitDto>()
                .ReverseMap();

            //Financials
            CreateMap<Expense, ExpenseDto>()
                .ReverseMap();
            CreateMap<Investment, InvestmentDto>()
                .ReverseMap();
            CreateMap<InvestmentSummary, InvestmentSummaryDto>()
                .ReverseMap();
            CreateMap<TotalSales, TotalSalesDto>()
                .ReverseMap();
            CreateMap<TotalSales, TotalSalesDto>()
                .ReverseMap();

            //Poultry
            CreateMap<BirdManagement, BirdManagementDto>()
                .ReverseMap();
            CreateMap<Birds, BirdsDto>()
                .ReverseMap();
            CreateMap<BirdTransaction, BirdTransactionDto>()
                .ReverseMap();
            CreateMap<EggInventory, EggInventoryDto>()
                .ReverseMap();
            CreateMap<EggManagement, EggManagementDto>()
                .ReverseMap();
            CreateMap<EggTransaction, EggTransactionDto>()
                .ReverseMap();
        }
    }
}
