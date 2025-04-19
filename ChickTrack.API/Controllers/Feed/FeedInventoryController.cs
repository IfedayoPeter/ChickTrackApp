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
        private readonly IFeedInventoryService _service;
        public FeedInventoryController(IFeedInventoryService service) : base(service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult> CreateFeedInventory([FromBody] FeedInventoryDto request)
        {
            var response = await _service.CreateAsync(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateFeedInventory(long id, [FromBody] FeedInventoryDto request)
        {
            var response = await _service.UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveFeedInventory(long id)
        {
            var response = await _service.DeleteAsync(id);
            return Ok(response);
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] FeedInventoryDto[] requests)
        {
            var response = await _service.ImportAsync(requests);
            return Ok(response);
        }
    }
}
