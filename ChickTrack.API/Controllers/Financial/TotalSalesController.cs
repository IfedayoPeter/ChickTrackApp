using ChickTrack.Domain.DataTransferObjects.Financial.GetDtos;
using ChickTrack.Domain.Entities.Financials;
using ChickTrack.Service.Interfaces.Financial;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers.Financial
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalSalesController : MSSQLBaseController<TotalSales, TotalSalesDto, long>
    {
        public TotalSalesController(ITotalSalesService service) : base(service)
        {
        }
        [HttpPost]
        public async Task<ActionResult> CreateTotalSales([FromBody] TotalSalesDto request)
        {
            var response = await CreateAsync(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateTotalSales(long id, [FromBody] TotalSalesDto request)
        {
            var response = await UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveTotalSales(long id)
        {
            var response = await RemoveAsync(id);
            return Ok(response);
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] TotalSalesDto[] requests)
        {
            var response = await ImportAsync(requests);
            return Ok(response);
        }
    }
}
