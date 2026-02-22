namespace API.Controllers.Feed
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedMeasurementPriceController : ControllerBase
    {
        private readonly IFeedMeasurementPriceService _feedMeasurementPriceService;

        public FeedMeasurementPriceController(IFeedMeasurementPriceService feedMeasurementPriceService)
        {
            _feedMeasurementPriceService = feedMeasurementPriceService;
        }

        [HttpGet("calculate")]
        public async Task<ActionResult<FeedMeasurementPricesDto>> Calculate(
            [FromQuery] decimal bagPrice,
            [FromQuery] FeedMeasurementCalculationModeEnum calculationMode = FeedMeasurementCalculationModeEnum.UseReferencePrices,
            [FromQuery] decimal profitPercentage = 35m)
        {
            if (bagPrice <= 0)
            {
                return BadRequest("bagPrice must be greater than 0.");
            }
            if (profitPercentage < 0)
            {
                return BadRequest("profitPercentage cannot be less than 0.");
            }

            var response = await _feedMeasurementPriceService.CalculateAsync(bagPrice, calculationMode, profitPercentage);
            return Ok(response);
        }
    }
}
