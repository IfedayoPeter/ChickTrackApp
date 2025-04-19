using AutoMapper;
using ChickTrack.Domain.DataTransferObjects.Feed;
using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Domain.Enums;
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

        public async Task<Result<FeedInventoryDto>> CreateAsync(FeedInventoryDto feedInventoryDto)
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

        public async Task<Result<FeedInventoryDto>> UpdateAsync(int id, FeedInventoryDto updatedDto)
        {
            var result = new Result<FeedInventoryDto>(false);
            try
            {
                var existingInventory = await _feedInventory.GetSingleAsync(x => x.Id == id);
                if (existingInventory == null)
                {
                    result.SetError("Feed inventory not found", "No feed inventory record exists with the provided ID.");
                    return result;
                }

                var oldBagsBought = existingInventory.BagsBought;

                // Update inventory properties
                _mapper.Map(updatedDto, existingInventory);
                await _feedInventory.UpdateAsync(id, existingInventory);

                var feedLog = await _feedLog.GetSingleAsync(x => x.FeedBrand == existingInventory.FeedBrand);
                if (feedLog != null)
                {
                    // Adjust FeedLog bags bought
                    feedLog.BagsBought = feedLog.BagsBought - oldBagsBought + updatedDto.BagsBought;
                    feedLog.AvailableBags = feedLog.BagsBought - feedLog.BagsSold;

                    await _feedLog.UpdateAsync(feedLog.Id, feedLog);
                }

                await _context.SaveChangesAsync();

                result.SetSuccess(_mapper.Map<FeedInventoryDto>(existingInventory), "Feed Inventory updated successfully!");
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while updating Feed Inventory");
            }

            return result;
        }

        public async Task<Result<bool>> DeleteAsync(long id)
        {
            var result = new Result<bool>(false);
            try
            {
                var inventory = await _feedInventory.GetSingleAsync(x => x.Id == id);
                if (inventory == null)
                {
                    result.SetError("Feed inventory not found", "No feed inventory record exists with the provided ID.");
                    return result;
                }

                var feedLog = await _feedLog.GetSingleAsync(x => x.FeedBrand == inventory.FeedBrand);
                if (feedLog != null)
                {
                    feedLog.BagsBought -= inventory.BagsBought;
                    feedLog.AvailableBags = feedLog.BagsBought - feedLog.BagsSold;

                    await _feedLog.UpdateAsync(feedLog.Id, feedLog);
                }

                await _feedInventory.DeleteAsync(id);
                await _context.SaveChangesAsync();

                result.SetSuccess(true, "Feed Inventory deleted successfully.");
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while deleting Feed Inventory");
            }

            return result;
        }

        public async Task<Result<List<long>>> ImportAsync(FeedInventoryDto[] feedInventoryDtos)
        {
            var result = new Result<List<long>>(false);
            var importedIds = new List<long>();

            try
            {
                if (feedInventoryDtos == null || feedInventoryDtos.Length == 0)
                {
                    result.SetError("No data to import", "The import list is empty.");
                    return result;
                }

                var feedLogUpdates = new Dictionary<string, decimal>(); // Fix for CS8619: Changed value type to decimal  

                foreach (var dto in feedInventoryDtos)
                {
                    dto.Code = RandomGenerator.RandomString(10);

                    var entity = _mapper.Map<FeedInventory>(dto);
                    var created = await _feedInventory.CreateAsync(entity);

                    if (created != null)
                    {
                        importedIds.Add(created.Id);
                    }

                    var feedBrandKey = dto.FeedBrand.ToString(); // Fix for CS1503: Convert FeedBrandEnum to string  
                    if (feedLogUpdates.ContainsKey(feedBrandKey))
                        feedLogUpdates[feedBrandKey] += dto.BagsBought;
                    else
                        feedLogUpdates[feedBrandKey] = dto.BagsBought;
                }

                foreach (var kvp in feedLogUpdates)
                {
                    var brand = kvp.Key;
                    var bagsToAdd = kvp.Value;

                    var feedLog = await _feedLog.GetSingleAsync(x => x.FeedBrand.ToString() == brand); // Fix for CS1503: Compare string representation  

                    if (feedLog == null)
                    {
                        feedLog = new FeedLog
                        {
                            Code = RandomGenerator.RandomString(10),
                            FeedBrand = Enum.Parse<FeedBrandEnum>(brand), // Convert string back to FeedBrandEnum  
                            BagsBought = bagsToAdd,
                            BagsSold = 0,
                            AvailableBags = bagsToAdd
                        };
                        await _feedLog.CreateAsync(feedLog);
                    }
                    else
                    {
                        feedLog.BagsBought += bagsToAdd;
                        feedLog.AvailableBags = feedLog.BagsBought - feedLog.BagsSold;
                        await _feedLog.UpdateAsync(feedLog.Id, feedLog);
                    }
                }

                await _context.SaveChangesAsync();

                result.SetSuccess(importedIds, "Feed inventory imported successfully.");
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while importing Feed Inventory");
            }

            return result;
        }



    }
}
