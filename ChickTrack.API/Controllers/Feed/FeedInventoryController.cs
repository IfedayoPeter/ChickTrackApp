using ChickTrack.Domain.DataTransferObjects.Feed;
using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Service.Interfaces.Feed;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers.Feed
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedInventoryController : MSSQLBaseController<FeedInventory, FeedInventoryDto, long>
    {
        public FeedInventoryController(IFeedInventoryService service) : base(service)
        {
        }
        [HttpPost]
        public async Task<ActionResult> CreateFeedInventory([FromBody] FeedInventoryDto request)
        {
            var response = await CreateAsync(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateFeedInventory(long id, [FromBody] FeedInventoryDto request)
        {
            var response = await UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveFeedInventory(long id)
        {
            var response = await RemoveAsync(id);
            return Ok(response);
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] FeedInventoryDto[] requests)
        {
            var response = await ImportAsync(requests);
            return Ok(response);
        }
    }
}
