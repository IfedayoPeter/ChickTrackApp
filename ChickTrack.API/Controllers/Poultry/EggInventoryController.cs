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
    public class EggInventoryController : MSSQLBaseController<EggInventory, EggInventoryDto, long>
    {
        public EggInventoryController(IEggInventoryService service) : base(service)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateEggInventory([FromBody] CreateEggInventoryDto request)
        {

            var response = await CreateAsync(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEggInventory(long id, [FromBody] UpdateEggInventoryDto request)
        {

            var response = await UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveEggInventory(long id)
        {

            var response = await RemoveAsync(id);

            return Ok(response);
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] EggInventoryDto[] requests)
        {

            var response = await ImportAsync(requests);

            return Ok(response);
        }
    }
}
