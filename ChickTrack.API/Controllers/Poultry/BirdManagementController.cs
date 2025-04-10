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
    public class BirdManagementController : MSSQLBaseController<BirdManagement, BirdManagementDto, long>
    {
        public BirdManagementController(IBirdManagementService service) : base(service)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateBirdManagement([FromBody] CreateBirdManagementDto request)
        {

            var response = await CreateAsync(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBirdManagement(long id, [FromBody] UpdateBirdManagementDto request)
        {

            var response = await UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBirdManagement(long id)
        {

            var response = await RemoveAsync(id);

            return Ok(response);
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] BirdManagementDto[] requests)
        {

            var response = await ImportAsync(requests);

            return Ok(response);
        }
    }
}
