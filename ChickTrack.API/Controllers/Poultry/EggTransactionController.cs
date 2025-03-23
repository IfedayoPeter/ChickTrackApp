using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Service.Interfaces.Poultry;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EggTransactionController : MSSQLBaseController<EggTransaction, EggTransactionDto, long>
    {
        public EggTransactionController(IEggTransactionService service) : base(service)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateEggTransaction([FromBody] EggTransactionDto request)
        {

            var response = await CreateAsync(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEggTransaction(long id, [FromBody] EggTransactionDto request)
        {

            var response = await UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveEggTransaction(long id)
        {

            var response = await RemoveAsync(id);

            return Ok(response);
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] EggTransactionDto[] requests)
        {

            var response = await ImportAsync(requests);

            return Ok(response);
        }
    }
}
