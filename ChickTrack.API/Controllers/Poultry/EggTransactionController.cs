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
    public class EggTransactionController : MSSQLBaseController<EggTransaction, EggTransactionDto, long>
    {
        private readonly IEggTransactionService _service;
        public EggTransactionController(IEggTransactionService service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateEggTransaction([FromBody] CreateEggTransactionDto request)
        {

            var response = await _service.CreateAsync(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEggTransaction(long id, [FromBody] UpdateEggTransactionDto request)
        {

            var response = await _service.UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveEggTransaction(long id)
        {

            var response = await _service.DeleteAsync(id);

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
