using AutoMapper;
using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Service.Interfaces.Poultry;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Services.Implementation;
using ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Domain.Utilities;
using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.Enums;

namespace ChickTrack.Service.Implementations.Poultry
{
    public class EggTransactionService : MSSQLBaseService<EggTransaction, long>, IEggTransactionService
    {
        private readonly IMSSQLRepository<EggTransaction, long> _eggTransactionRepository;
        private readonly IMSSQLRepository<EggInventory, long> _eggInventoryRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EggTransactionService(
            IMSSQLRepository<EggTransaction, long> eggTransaction,
            IMSSQLRepository<EggInventory, long> eggInventory,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(eggTransaction, context, mapper)
        {
            _eggTransactionRepository = eggTransaction;
            _eggInventoryRepository = eggInventory;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<EggTransactionDto>> CreateEggTransaction(CreateEggTransactionDto eggTransactionDto)
        {
            var result = new Result<EggTransactionDto>(false);
            try
            {
                eggTransactionDto.Code = RandomGenerator.RandomString(10);
                var eggTransaction = _mapper.Map<EggTransaction>(eggTransactionDto);
                var response = await _eggTransactionRepository.CreateAsync(eggTransaction);

                if ( eggTransactionDto.ActionType == ActionTypeEnum.PersonalConsumption)
                {
                    var eggInventory = await _eggInventoryRepository.GetSingleAsync(x => x.InvestorId == eggTransactionDto.InvestorId);


                    if (eggInventory == null)
                    {
                        eggInventory = new EggInventory
                        {
                            Code = RandomGenerator.RandomString(10),
                            InvestorId = eggTransactionDto.InvestorId,
                            Sold = 0,
                            Hatched = 0,
                            PersonalConsumption = eggTransactionDto.ActionType == ActionTypeEnum.PersonalConsumption ? eggTransactionDto.Quantity : 0,
                        };
                        await _eggInventoryRepository.CreateAsync(eggInventory);
                    }
                    else
                    {
                        eggInventory.PersonalConsumption = eggTransactionDto.ActionType == ActionTypeEnum.PersonalConsumption ? eggTransactionDto.Quantity : 0,
                        await _eggInventoryRepository.UpdateAsync(eggInventory.Id, eggInventory);
                    }
                }
                
                if (response == null)
                {
                    result.SetError($"{typeof(EggTransaction).Name} not created", $"{typeof(EggTransaction).Name} not created");
                }
                else
                {
                    result.SetSuccess(_mapper.Map<EggTransactionDto>(response), $"{typeof(EggTransaction).Name} created successfully!");
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), $"Error while creating {typeof(EggTransaction).Name}");
            }
            return result;
        }

        public async Task<Result<bool>> DeleteAsync(long id)
        {
            var result = new Result<bool>(false);
            try
            {
                var response = await _eggTransactionRepository.DeleteAsync(id);
                if (response == null)
                {
                    result.SetError($"{typeof(EggTransaction).Name} not deleted", $"{typeof(EggTransaction).Name} not deleted");
                }
                else
                {
                    result.SetSuccess(true, $"{typeof(EggTransaction).Name} deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), $"Error while deleting {typeof(EggTransaction).Name}");
            }
            return result;
        }

        public async Task<Result<bool>> UpdateAsync(long id, EggTransactionDto eggTransactionDto)
        {
            var result = new Result<bool>(false);
            try
            {
                var eggTransaction = await _eggTransactionRepository.GetSingleAsync(x => x.Id == id);
                if (eggTransaction == null)
                {
                    result.SetError($"{typeof(EggTransaction).Name} not found", $"{typeof(EggTransaction).Name} not found");
                    return result;
                }
                eggTransaction.EggType = eggTransactionDto.EggType;
                eggTransaction.EggsBought = eggTransactionDto.EggsBought;
                eggTransaction.EggsSold = eggTransactionDto.EggsSold;
                eggTransaction.AvailableEggs = eggTransactionDto.AvailableEggs;
                var response = await _eggTransactionRepository.UpdateAsync(eggTransaction);
                if (response == null)
                {
                    result.SetError($"{typeof(EggTransaction).Name} not updated", $"{typeof(EggTransaction).Name} not updated");
                }
                else
                {
                    result.SetSuccess(true, $"{typeof(EggTransaction).Name} updated successfully!");
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), $"Error while updating {typeof(EggTransaction).Name}");
            }
            return result;
        }

    }
}
