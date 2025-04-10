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
    public class InvestmentSummaryController : MSSQLBaseController<InvestmentSummary, InvestmentSummaryDto, long>
    {
        public InvestmentSummaryController(IInvestmentSummaryService service) : base(service)
        {
        }
        [HttpPost]
        public async Task<ActionResult> CreateInvestmentSummary([FromBody] CreateInvestmentSummaryDto request)
        {
            var response = await CreateAsync(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateInvestmentSummary(long id, [FromBody] UpdateInvestmentSummaryDto request)
        {
            var response = await UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveInvestmentSummary(long id)
        {
            var response = await RemoveAsync(id);
            return Ok(response);
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] InvestmentSummaryDto[] requests)
        {
            var response = await ImportAsync(requests);
            return Ok(response);
        }
    }
}
