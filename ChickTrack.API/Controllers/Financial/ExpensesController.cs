using ChickTrack.Domain.DataTransferObjects.Financial;
using ChickTrack.Domain.DataTransferObjects.Financial.GetDtos;
using ChickTrack.Domain.DataTransferObjects.Financial.UpdateDtos;
using ChickTrack.Domain.Entities.Financials;
using ChickTrack.Service.Interfaces.Financial;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers.Financial
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : MSSQLBaseController<Expense, ExpenseDto, long>
    {
        public ExpensesController(IExpensesService service) : base(service)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateExpense([FromBody] CreateExpenseDto request)
        {

            var response = await CreateAsync(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateExpense(long id, [FromBody] UpdateExpenseDto request)
        {

            var response = await UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveExpense(long id)
        {

            var response = await RemoveAsync(id);

            return Ok(response);
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] ExpenseDto[] requests)
        {

            var response = await ImportAsync(requests);

            return Ok(response);
        }
    }
}
