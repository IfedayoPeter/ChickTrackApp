using ChickTrack.Domain.DataTransferObjects.Feed;
using ChickTrack.Domain.DataTransferObjects.Financial;
using ChickTrack.Domain.DataTransferObjects.Financial.CreateDtos;
using ChickTrack.Domain.DataTransferObjects.Financial.GetDtos;
using ChickTrack.Domain.DataTransferObjects.Financial.UpdateDtos;
using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos;
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
            CreateMap<FeedPrice, FeedPriceDto>()
                .ReverseMap();
            CreateMap<FeedUnitPrice, FeedUnitPriceDto>()
                .ReverseMap();

            //Financials
            CreateMap<Expense, ExpenseDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Investor.FullName))
                .ReverseMap();
            CreateMap<Expense, CreateExpenseDto>()
                .ReverseMap();
            CreateMap<Expense, UpdateExpenseDto>()
                .ReverseMap();
            CreateMap<Investment, CreateInvestmentDto>()
                .ReverseMap();
            CreateMap<Investment, UpdateInvestmentDto>()
                .ReverseMap();
            CreateMap<Investment, InvestmentDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Investor.FullName))
                .ReverseMap();
            CreateMap<SaleRecord, SaleRecordDto>()
                .ReverseMap();
            CreateMap<SaleRecord, CreateSaleRecordDto>()
                .ReverseMap();
            CreateMap<TotalSales, TotalSalesDto>()
                .ReverseMap();

            //Poultry
            CreateMap<Birds, BirdsDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Investor.FullName))
                .ReverseMap();
            CreateMap<BirdTransaction, BirdTransactionDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Investor.FullName))
                .ReverseMap();
            CreateMap<Birds, CreateBirdsDto>()
                .ReverseMap();
            CreateMap<BirdTransaction, CreateBirdTransactionDto>()
                .ReverseMap();
            CreateMap<EggInventory, CreateEggInventoryDto>()
                .ReverseMap();
            CreateMap<EggInventory, EggInventoryDto>()
                .ReverseMap();
            CreateMap<EggTransaction, CreateEggTransactionDto>()
                .ReverseMap();
            CreateMap<EggTransaction, EggTransactionDto>()
                .ReverseMap();
        }
    }
}
