using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Service.Interfaces.Poultry;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : MSSQLBaseController<Birds, BirdsDto, long>
    {
        public BirdsController(IBirdService service) : base(service)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateBirds([FromBody] BirdsDto request)
        {

            var response = await CreateAsync(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBirds(long id, [FromBody] BirdsDto request)
        {

            var response = await UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBirds(long id)
        {

            var response = await RemoveAsync(id);

            return Ok(response);
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] BirdsDto[] requests)
        {

            var response = await ImportAsync(requests);

            return Ok(response);
        }
    }
}
