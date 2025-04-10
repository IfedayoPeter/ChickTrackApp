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
    public class InvestmentController : MSSQLBaseController<Investment, InvestmentDto, long>
    {
        public InvestmentController(IInvestmentService service) : base(service)
        {
        }
        [HttpPost]
        public async Task<ActionResult> CreateInvestment([FromBody] CreateInvestmentDto request)
        {
            var response = await CreateAsync(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateInvestment(long id, [FromBody] UpdateInvestmentDto request)
        {
            var response = await UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveInvestment(long id)
        {
            var response = await RemoveAsync(id);
            return Ok(response);
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] InvestmentDto[] requests)
        {
            var response = await ImportAsync(requests);
            return Ok(response);
        }
    }
}
