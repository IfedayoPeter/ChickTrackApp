using ChickTrack.Domain.DataTransferObjects.Feed;
using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Service.Interfaces.Feed;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers.Feed
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedPriceController : MSSQLBaseController<FeedPrice, FeedPriceDto, long>
    {
        private readonly IFeedPriceService _service;
        public FeedPriceController(IFeedPriceService service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateFeedPrice([FromBody] FeedPriceDto request)
        {
            var response = await CreateAsync (request);
            return Ok(response);
        }

        [HttpGet("active")]
        public async Task<ActionResult> GetActiveFeedPrices()
        {
            var response = await _service.GetActivePricesAsync();
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateFeedPrice(long id, [FromBody] FeedPriceDto request)
        {
            var response = await UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveFeedPrice(long id)
        {
            var response = await RemoveAsync(id);
            return Ok(response);
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] FeedPriceDto[] requests)
        {
            var response = await ImportAsync(requests);
            return Ok(response);
        }
    }
}
