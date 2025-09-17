
namespace API.Controllers.Feed
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedUnitPriceController : MSSQLBaseController<FeedUnitPrice, FeedUnitPriceDto, long>
    {
        private readonly IFeedUnitPriceService _service;
        public FeedUnitPriceController(IFeedUnitPriceService service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateFeedUnitPrice([FromBody] FeedUnitPriceDto request)
        {
            var response = await CreateAsync(request);
            return Ok(response);
        }

        [HttpGet("active")]
        public async Task<ActionResult> GetActiveFeedUnitPrices()
        {
            var response = await _service.GetActivePricesAsync();
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateFeedUnitPrice(long id, [FromBody] FeedUnitPriceDto request)
        {
            var response = await UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveFeedUnitPrice(long id)
        {
            var response = await RemoveAsync(id);
            return Ok(response);
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] FeedUnitPriceDto[] requests)
        {
            var response = await ImportAsync(requests);
            return Ok(response);
        }
    }
}
