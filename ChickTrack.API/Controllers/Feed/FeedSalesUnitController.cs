namespace API.Controllers.Feed
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedSalesUnitController : MSSQLBaseController<FeedSalesUnit, FeedSalesUnitDto, long>
    {
        public FeedSalesUnitController(IFeedSalesUnitService service) : base(service)
        {
        }
        [HttpPost]
        public async Task<ActionResult> CreateFeedSalesUnit([FromBody] FeedSalesUnitDto request)
        {
            var response = await CreateAsync(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateFeedSalesUnit(long id, [FromBody] FeedSalesUnitDto request)
        {
            var response = await UpdateAsync(id, request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveFeedSalesUnit(long id)
        {
            var response = await RemoveAsync(id);
            return Ok(response);
        }
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] FeedSalesUnitDto[] requests)
        {
            var response = await ImportAsync(requests);
            return Ok(response);
        }
    }
}
