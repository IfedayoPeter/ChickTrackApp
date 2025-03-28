using AutoMapper;
using ChickTrack.Domain.DataTransferObjects.Feed;
using ChickTrack.Domain.DataTransferObjects.Financial;
using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Service.Interfaces.Feed;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Domain.Utilities;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Services.Implementation;

namespace ChickTrack.Service.Implementations.Feed
{
    public class FeedInventoryService : MSSQLBaseService<FeedInventory, long>, IFeedInventoryService
    {
        private readonly IMSSQLRepository<FeedInventory, long> _feedInventory;
        private readonly IMSSQLRepository<FeedLog, long> _feedLog;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public FeedInventoryService(
            IMSSQLRepository<FeedInventory, long> feedInventory,
            IMSSQLRepository<FeedLog, long> feedLog,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(feedInventory, context, mapper)
        {
            _feedInventory = feedInventory;
            _feedLog = feedLog;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<FeedInventoryDto>> CreateFeedInventory(FeedInventoryDto feedInventoryDto)
        {
            var result = new Result<FeedInventoryDto>(false);
            try
            {
                feedInventoryDto.Code = RandomGenerator.RandomString(10);

                var feedInventory = _mapper.Map<FeedInventory>(feedInventoryDto);
                var response = await _feedInventory.CreateAsync(feedInventory);

                var feedLog = await _feedLog.GetSingleAsync(x => x.FeedBrand == feedInventoryDto.FeedBrand);

                if (feedLog == null)
                {
                    feedLog = new FeedLog
                    {
                        Code = RandomGenerator.RandomString(10),
                        FeedBrand = feedInventoryDto.FeedBrand,
                        BagsBought = feedInventoryDto.BagsBought,
                        BagsSold = 0,
                        AvailableBags = feedInventoryDto.BagsBought
                    };
                    await _feedLog.CreateAsync(feedLog);
                }
                else
                {
                    feedLog.BagsBought += feedInventoryDto.BagsBought;
                    feedLog.AvailableBags = feedLog.BagsBought - feedLog.BagsSold;
                    await _feedLog.UpdateAsync(feedLog.Id, feedLog);
                }

                await _context.SaveChangesAsync();

                if (response == null)
                {
                    result.SetError("Feed Inventory not created", "Feed Inventory not created");
                }
                else
                {
                    result.SetSuccess(_mapper.Map<FeedInventoryDto>(response), "Feed Inventory created successfully!");
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while creating Feed Inventory");
            }
            return result;
        }
    }
}
