using ChickTrack.Domain.DataTransferObjects;
using ChickTrack.Domain.Entities.Financials;
using ChickTrack.Service.Interfaces;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : MSSQLBaseController<Investment, InvestmentDTO, long>
    {
        public InvestmentController(IInvestmentService service) : base(service)
        {
        }
        [HttpPost]
        public async Task<ActionResult> CreateInvestment([FromBody] InvestmentDTO request)
        {
            var response = await CreateAsync(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateInvestment(long id, [FromBody] InvestmentDTO request)
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
        public async Task<ActionResult> Import([FromBody] InvestmentDTO[] requests)
        {
            var response = await ImportAsync(requests);
            return Ok(response);
        }
    }
}
