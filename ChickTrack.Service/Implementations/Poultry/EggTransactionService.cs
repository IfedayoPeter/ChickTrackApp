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
using Microsoft.Extensions.FileSystemGlobbing;

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

        public async Task<Result<EggTransactionDto>> CreateAsync(CreateEggTransactionDto eggTransactionDto)
        {
            var result = new Result<EggTransactionDto>(false);
            try
            {
                eggTransactionDto.Code = RandomGenerator.RandomString(10);
                var eggTransaction = _mapper.Map<EggTransaction>(eggTransactionDto);
                var response = await _eggTransactionRepository.CreateAsync(eggTransaction);

                await _context.SaveChangesAsync();
                var existingEggTransaction = await _eggTransactionRepository.GetSingleAsync(x => x.Id == response.Id);
                if (existingEggTransaction == null)
                {
                    result.SetError("Failed to record", "Transaction record was unsuccessful!");
                    return result;
                }
                else if (eggTransactionDto.ActionType == ActionTypeEnum.PersonalConsumption && string.IsNullOrEmpty(eggTransactionDto.InvestorId))
                {
                    result.SetError("Investor ID is required", "Investor ID is required for personal consumption transactions");
                    return result;
                }
                else
                {
                    var allInventories = await _eggInventoryRepository.GetAllAsync();
                    var eggInventory = allInventories.FirstOrDefault();

                    if (eggInventory == null)
                    {
                        eggInventory = new EggInventory
                        {
                            Code = RandomGenerator.RandomString(10),
                            Sold = eggTransactionDto.ActionType == ActionTypeEnum.Sell ? eggTransactionDto.Quantity : 0,
                            Hatched = eggTransactionDto.ActionType == ActionTypeEnum.Hatch ? eggTransactionDto.Quantity : 0,
                            PersonalConsumption = eggTransactionDto.ActionType == ActionTypeEnum.PersonalConsumption ? eggTransactionDto.Quantity : 0,
                            Stock = eggTransactionDto.ActionType == ActionTypeEnum.Add ? eggTransactionDto.Quantity : 0,
                        };
                        await _eggInventoryRepository.CreateAsync(eggInventory);
                    }
                    else
                    {
                        if (eggInventory.Stock < eggTransactionDto.Quantity)
                        { result.SetError("Insufficient stock", "Not enough stock to complete the transaction"); return result; }

                        switch (eggTransactionDto.ActionType)
                        {
                            case ActionTypeEnum.Sell:
                                eggInventory.Sold += eggTransactionDto.Quantity;
                                eggInventory.Stock -= eggTransactionDto.Quantity;
                                break;
                            case ActionTypeEnum.Hatch:
                                eggInventory.Hatched += eggTransactionDto.Quantity;
                                eggInventory.Stock -= eggTransactionDto.Quantity;
                                break;
                            case ActionTypeEnum.PersonalConsumption:
                                eggInventory.PersonalConsumption += eggTransactionDto.Quantity;
                                eggInventory.Stock -= eggTransactionDto.Quantity;
                                break;
                            case ActionTypeEnum.Add:
                                eggInventory.Stock += eggTransactionDto.Quantity;
                                break;
                            default:
                                break;
                        }

                        await _eggInventoryRepository.UpdateAsync(eggInventory.Id, eggInventory);
                    }
                    await _context.SaveChangesAsync();
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
                var transaction = await _eggTransactionRepository.GetSingleAsync(x => x.Id == id);
                if (transaction == null)
                {
                    result.SetError("Transaction not found", "No egg transaction with this ID");
                    return result;
                }

                var inventory = (await _eggInventoryRepository.GetAllAsync()).FirstOrDefault();
                if (inventory != null)
                {
                    switch (transaction.ActionType)
                    {
                        case ActionTypeEnum.PersonalConsumption:
                            inventory.PersonalConsumption -= transaction.Quantity;
                            inventory.Stock += transaction.Quantity;
                            break;
                        case ActionTypeEnum.Hatch:
                            inventory.Hatched -= transaction.Quantity;
                            inventory.Stock += transaction.Quantity;
                            break;
                        case ActionTypeEnum.Sell:
                            inventory.Sold -= transaction.Quantity;
                            inventory.Stock += transaction.Quantity;
                            break;
                        case ActionTypeEnum.Add:
                            inventory.Stock -= transaction.Quantity;
                            break;
                    }

                    await _eggInventoryRepository.UpdateAsync(inventory.Id, inventory);
                }

                await _eggTransactionRepository.DeleteAsync(id);
                await _context.SaveChangesAsync();

                result.SetSuccess(true, "Egg transaction deleted successfully");
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message, "Error deleting egg transaction");
            }

            return result;
        }


        public async Task<Result<bool>> UpdateAsync(long id, UpdateEggTransactionDto eggTransactionDto)
        {
            var result = new Result<bool>(false);
            try
            {
                var existing = await _eggTransactionRepository.GetSingleAsync(x => x.Id == id);
                if (existing == null)
                {
                    result.SetError("Transaction not found", "No egg transaction with this ID");
                    return result;
                }

                else if (eggTransactionDto.ActionType == ActionTypeEnum.PersonalConsumption && string.IsNullOrEmpty(eggTransactionDto.InvestorId))
                {
                    result.SetError("Investor ID is required", "Investor ID is required for personal consumption transactions");
                    return result;
                }

                var inventory = (await _eggInventoryRepository.GetAllAsync()).FirstOrDefault();
                if (inventory == null)
                {
                    result.SetError("Inventory not found", "No egg inventory available");
                    return result;
                }

                // Reverse old values
                switch (existing.ActionType)
                {
                    case ActionTypeEnum.PersonalConsumption:
                        inventory.PersonalConsumption -= existing.Quantity;
                        inventory.Stock += existing.Quantity;
                        break;
                    case ActionTypeEnum.Hatch:
                        inventory.Hatched -= existing.Quantity;
                        inventory.Stock += existing.Quantity;
                        break;
                    case ActionTypeEnum.Sell:
                        inventory.Sold -= existing.Quantity;
                        inventory.Stock += existing.Quantity;
                        break;
                    case ActionTypeEnum.Add:
                        inventory.Stock -= existing.Quantity;
                        break;
                }

                if (inventory.Stock < eggTransactionDto.Quantity)
                { result.SetError("Insufficient stock", "Not enough stock to complete the transaction"); return result; }

                // Apply new values
                switch (eggTransactionDto.ActionType)
                {
                    case ActionTypeEnum.PersonalConsumption:
                        inventory.PersonalConsumption += eggTransactionDto.Quantity;
                        inventory.Stock -= eggTransactionDto.Quantity;
                        break;
                    case ActionTypeEnum.Hatch:
                        inventory.Hatched += eggTransactionDto.Quantity;
                        inventory.Stock -= eggTransactionDto.Quantity;
                        break;
                    case ActionTypeEnum.Sell:
                        inventory.Sold += eggTransactionDto.Quantity;
                        inventory.Stock -= eggTransactionDto.Quantity;
                        break;
                    case ActionTypeEnum.Add:
                        inventory.Stock += eggTransactionDto.Quantity;
                        break;
                }

                // Update transaction
                _mapper.Map(eggTransactionDto, existing);
                await _eggTransactionRepository.UpdateAsync(id, existing);
                await _eggInventoryRepository.UpdateAsync(inventory.Id, inventory);
                await _context.SaveChangesAsync();

                result.SetSuccess(true, "Egg transaction updated successfully");
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message, "Error updating egg transaction");
            }

            return result;
        }
    }
}
