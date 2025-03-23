using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Service.Interfaces.Poultry;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdTransactionController : MSSQLBaseController<BirdTransaction, BirdTransactionDto, long>
    {
        public BirdTransactionController(IBirdTransactionService service) : base(service)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateBirdTransaction([FromBody] BirdTransactionDto request)
        {

            var response = await CreateAsync(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBirdTransaction(long id, [FromBody] BirdTransactionDto request)
        {

            var response = await UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBirdTransaction(long id)
        {

            var response = await RemoveAsync(id);

            return Ok(response);
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] BirdTransactionDto[] requests)
        {

            var response = await ImportAsync(requests);

            return Ok(response);
        }
    }
}
