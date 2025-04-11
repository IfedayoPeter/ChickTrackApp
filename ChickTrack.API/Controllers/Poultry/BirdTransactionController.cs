using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos;
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
        private readonly IBirdTransactionService _service;
        public BirdTransactionController(IBirdTransactionService service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBirdTransaction([FromBody] CreateBirdTransactionDto request)
        {

            var response = await _service.CreateAsync(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBirdTransaction(long id, [FromBody] UpdateBirdTransactionDto request)
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
