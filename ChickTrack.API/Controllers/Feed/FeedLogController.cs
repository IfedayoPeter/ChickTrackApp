using ChickTrack.Domain.DataTransferObjects.Feed;
using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Service.Interfaces.Feed;
using Lagetronix.Rapha.Base.Common.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace ChickTrack.API.Controllers.Feed
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedLogController : MSSQLBaseController<FeedLog, FeedLogDto, long>
    {
        public FeedLogController(IFeedLogService service) : base(service)
        {
        }
        [HttpPost]
        public async Task<ActionResult> CreateFeedLog([FromBody] FeedLogDto request)
        {
            var response = await CreateAsync(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateFeedLog(long id, [FromBody] FeedLogDto request)
        {
            var response = await UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveFeedLog(long id)
        {
            var response = await RemoveAsync(id);
            return Ok(response);
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] FeedLogDto[] requests)
        {
            var response = await ImportAsync(requests);
            return Ok(response);
        }
    }
}
