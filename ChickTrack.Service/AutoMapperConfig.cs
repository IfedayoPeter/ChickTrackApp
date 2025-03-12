using ChickTrack.Domain.DataTransferObjects;
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
            CreateMap<Expense, ExpenseDTO>()
                .ReverseMap();
            CreateMap<Investment, InvestmentDTO>()
                .ReverseMap();
            CreateMap<Birds, PoultryDTO>()
                .ReverseMap();
        }
    }
}
