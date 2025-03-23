using ChickTrack.Domain.DataTransferObjects.Financial;
using ChickTrack.Domain.Entities.Financials;
using ChickTrack.Service.Interfaces.Financial;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers.Financial
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleRecordController : MSSQLBaseController<SaleRecord, SaleRecordDto, long>
    {
        public SaleRecordController(ISaleRecordService service) : base(service)
        {
        }
        [HttpPost]
        public async Task<ActionResult> CreateSaleRecord([FromBody] SaleRecordDto request)
        {
            var response = await CreateAsync(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateSaleRecord(long id, [FromBody] SaleRecordDto request)
        {
            var response = await UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveSaleRecord(long id)
        {
            var response = await RemoveAsync(id);
            return Ok(response);
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] SaleRecordDto[] requests)
        {
            var response = await ImportAsync(requests);
            return Ok(response);
        }
    }
}
