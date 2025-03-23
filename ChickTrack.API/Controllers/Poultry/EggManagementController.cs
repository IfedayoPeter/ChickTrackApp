using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Service.Interfaces.Poultry;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EggManagementController : MSSQLBaseController<EggManagement, EggManagementDto, long>
    {
        public EggManagementController(IEggManagementService service) : base(service)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateEggManagement([FromBody] EggManagementDto request)
        {

            var response = await CreateAsync(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEggManagement(long id, [FromBody] EggManagementDto request)
        {

            var response = await UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveEggManagement(long id)
        {

            var response = await RemoveAsync(id);

            return Ok(response);
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] EggManagementDto[] requests)
        {

            var response = await ImportAsync(requests);

            return Ok(response);
        }
    }
}
