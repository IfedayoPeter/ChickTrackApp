using AutoMapper;
using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Service.Interfaces.Poultry;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Services.Implementation;
using ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos;
using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Domain.Utilities;

namespace ChickTrack.Service.Implementations.Poultry
{
    public class BirdTransactionService : MSSQLBaseService<BirdTransaction, long>, IBirdTransactionService
    {
        private readonly IMSSQLRepository<BirdTransaction, long> _birdTransaction;
        private readonly IMSSQLRepository<Birds, long> _birds;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BirdTransactionService(
            IMSSQLRepository<BirdTransaction, long> birdTransaction,
            IMSSQLRepository<Birds, long> birds,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(birdTransaction, context, mapper)
        {
            _birdTransaction = birdTransaction;
            _birds = birds;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<BirdTransactionDto>> CreateAsync(CreateBirdTransactionDto birdTransactionDto)
        {
            var result = new Result<BirdTransactionDto>(false);
            try
            {
                birdTransactionDto.Code = RandomGenerator.RandomString(10);

                var birdTransaction = _mapper.Map<BirdTransaction>(birdTransactionDto);
                var response = await _birdTransaction.CreateAsync(birdTransaction);

                await _context.SaveChangesAsync();
                // Check if the bird transaction was created successfully
                var existingBirdTransaction = await _birdTransaction.GetSingleWithIncludeAsync(x => x.Code == birdTransactionDto.Code, x => x.Investor);
                if (existingBirdTransaction == null)
                {
                    result.SetError("Bird Transaction not created", "Bird Transaction not created");
                }
                else
                {
                    // If the bird transaction was created successfully, create a new bird and bird transaction
                    var birds = await _birds.GetSingleAsync(x => x.InvestorId == birdTransactionDto.InvestorId);

                    if (birds == null)
                    {
                        birds = new Birds
                        {
                            Code = RandomGenerator.RandomString(10),
                            InvestorId = birdTransactionDto.InvestorId,
                            MaleBirds = birdTransactionDto.Gender == GenderEnum.Male ? birdTransactionDto.Quantity : 0,
                            FemaleBirds = birdTransactionDto.Gender == GenderEnum.Female ? birdTransactionDto.Quantity : 0,
                            Chicks = birdTransactionDto.Gender == GenderEnum.Chicks ? birdTransactionDto.Quantity : 0,
                            BirdsSold = birdTransactionDto.ActionType == ActionTypeEnum.Sell ? birdTransactionDto.Quantity : 0,
                            BirdsLost = birdTransactionDto.ActionType == ActionTypeEnum.Loss ? birdTransactionDto.Quantity : 0,
                            PersonalConsumption = birdTransactionDto.ActionType == ActionTypeEnum.PersonalConsumption ? birdTransactionDto.Quantity : 0,
                            HatchedBirds = birdTransactionDto.ActionType == ActionTypeEnum.Hatch ? birdTransactionDto.Quantity : 0,
                        };
                        await _birds.CreateAsync(birds);
                    }
                    else
                    {
                        // Update based on Gender
                        switch (birdTransactionDto.Gender)
                        {
                            case GenderEnum.Male:
                                birds.MaleBirds += birdTransactionDto.Quantity;
                                break;
                            case GenderEnum.Female:
                                birds.FemaleBirds += birdTransactionDto.Quantity;
                                break;
                            case GenderEnum.Chicks:
                                birds.Chicks += birdTransactionDto.Quantity;
                                break;
                        }
                        // Update based on Action Type
                        switch (birdTransactionDto.ActionType)
                        {
                            case ActionTypeEnum.Sell:
                                birds.BirdsSold += birdTransactionDto.Quantity;
                                break;
                            case ActionTypeEnum.Loss:
                                birds.BirdsLost += birdTransactionDto.Quantity;
                                break;
                            case ActionTypeEnum.Hatch:
                                birds.HatchedBirds += birdTransactionDto.Quantity;
                                break;
                            case ActionTypeEnum.PersonalConsumption:
                                birds.PersonalConsumption += birdTransactionDto.Quantity;
                                break;
                        }
                        await _birds.UpdateAsync(birds.Id, birds);
                    }
                }
                var newResponse = await _context.SaveChangesAsync();
                if (newResponse == null)
                {
                    result.SetError($"{typeof(BirdTransaction).Name} not created", $"{typeof(BirdTransaction).Name} not created");
                }
                else
                {
                    result.SetSuccess(_mapper.Map<BirdTransactionDto>(response), $"{typeof(BirdTransaction).Name} created successfully!");
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while creating Bird Transaction");
            }
            return result;
        }

        public async Task<Result<bool>> DeleteAsync(long id)
        {
            var result = new Result<bool>(false);
            try
            {
                var birdTransaction = await _birdTransaction.GetSingleAsync(x => x.Id == id);

                if (birdTransaction == null)
                {
                    result.SetError("Bird Transaction not found", "Transaction does not exist.");
                    return result;
                }

                var birds = await _birds.GetSingleAsync(x => x.InvestorId == birdTransaction.InvestorId);

                if (birds == null)
                {
                    result.SetError("Birds record not found", "Associated birds record does not exist.");
                    return result;
                }

                // Revert bird counts based on the transaction being deleted
                switch (birdTransaction.Gender)
                {
                    case GenderEnum.Male:
                        birds.MaleBirds = Math.Max(0, birds.MaleBirds - birdTransaction.Quantity);
                        break;
                    case GenderEnum.Female:
                        birds.FemaleBirds = Math.Max(0, birds.FemaleBirds - birdTransaction.Quantity);
                        break;
                    case GenderEnum.Chicks:
                        birds.Chicks = Math.Max(0, birds.Chicks - birdTransaction.Quantity);
                        break;
                }

                switch (birdTransaction.ActionType)
                {
                    case ActionTypeEnum.Sell:
                        birds.BirdsSold = Math.Max(0, birds.BirdsSold - birdTransaction.Quantity);
                        break;
                    case ActionTypeEnum.Loss:
                        birds.BirdsLost = Math.Max(0, birds.BirdsLost - birdTransaction.Quantity);
                        break;
                    case ActionTypeEnum.Hatch:
                        birds.HatchedBirds = Math.Max(0, birds.HatchedBirds - birdTransaction.Quantity);
                        break;
                    case ActionTypeEnum.PersonalConsumption:
                        birds.PersonalConsumption = Math.Max(0, birds.PersonalConsumption - birdTransaction.Quantity);
                        break;
                }

                await _birds.UpdateAsync(birds.Id, birds);
                await _birdTransaction.DeleteAsync(id);
                await _context.SaveChangesAsync();

                result.SetSuccess(true, "Bird Transaction deleted and records updated successfully.");
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message, "Error while deleting Bird Transaction");
            }

            return result;
        }

        public async Task<Result<bool>> UpdateAsync(long id, UpdateBirdTransactionDto birdTransactionDto)
        {
            var result = new Result<bool>(false);
            try
            {
                var existingTransaction = await _birdTransaction.GetSingleAsync(x => x.Id == id);
                if (existingTransaction == null)
                {
                    result.SetError("Transaction not found", "Bird transaction not found.");
                    return result;
                }

                var birds = await _birds.GetSingleAsync(x => x.InvestorId == existingTransaction.InvestorId);
                if (birds == null)
                {
                    result.SetError("Birds record not found", "Associated birds record does not exist.");
                    return result;
                }

                // REVERT OLD TRANSACTION EFFECTS
                switch (existingTransaction.Gender)
                {
                    case GenderEnum.Male:
                        birds.MaleBirds = Math.Max(0, birds.MaleBirds - existingTransaction.Quantity);
                        break;
                    case GenderEnum.Female:
                        birds.FemaleBirds = Math.Max(0, birds.FemaleBirds - existingTransaction.Quantity);
                        break;
                    case GenderEnum.Chicks:
                        birds.Chicks = Math.Max(0, birds.Chicks - existingTransaction.Quantity);
                        break;
                }

                switch (existingTransaction.ActionType)
                {
                    case ActionTypeEnum.Sell:
                        birds.BirdsSold = Math.Max(0, birds.BirdsSold - existingTransaction.Quantity);
                        break;
                    case ActionTypeEnum.Loss:
                        birds.BirdsLost = Math.Max(0, birds.BirdsLost - existingTransaction.Quantity);
                        break;
                    case ActionTypeEnum.Hatch:
                        birds.HatchedBirds = Math.Max(0, birds.HatchedBirds - existingTransaction.Quantity);
                        break;
                    case ActionTypeEnum.PersonalConsumption:
                        birds.PersonalConsumption = Math.Max(0, birds.PersonalConsumption - existingTransaction.Quantity);
                        break;
                }

                // UPDATE TRANSACTION FIELDS
                existingTransaction.Quantity = birdTransactionDto.Quantity;
                existingTransaction.ActionType = birdTransactionDto.ActionType;
                existingTransaction.Gender = birdTransactionDto.Gender;
                existingTransaction.Description = birdTransactionDto.Description;
                existingTransaction.Date = birdTransactionDto.Date;

                // APPLY NEW TRANSACTION EFFECTS
                switch (existingTransaction.Gender)
                {
                    case GenderEnum.Male:
                        birds.MaleBirds += existingTransaction.Quantity;
                        break;
                    case GenderEnum.Female:
                        birds.FemaleBirds += existingTransaction.Quantity;
                        break;
                    case GenderEnum.Chicks:
                        birds.Chicks += existingTransaction.Quantity;
                        break;
                }

                switch (existingTransaction.ActionType)
                {
                    case ActionTypeEnum.Sell:
                        birds.BirdsSold += existingTransaction.Quantity;
                        break;
                    case ActionTypeEnum.Loss:
                        birds.BirdsLost += existingTransaction.Quantity;
                        break;
                    case ActionTypeEnum.Hatch:
                        birds.HatchedBirds += existingTransaction.Quantity;
                        break;
                    case ActionTypeEnum.PersonalConsumption:
                        birds.PersonalConsumption += existingTransaction.Quantity;
                        break;
                }

                await _birdTransaction.UpdateAsync(existingTransaction.Id, existingTransaction);
                await _birds.UpdateAsync(birds.Id, birds);
                await _context.SaveChangesAsync();

                result.SetSuccess(true, "Bird Transaction updated successfully.");
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message, "Error while updating Bird Transaction.");
            }

            return result;
        }

    }
}
