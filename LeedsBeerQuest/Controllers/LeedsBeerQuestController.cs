using LeedsBeerQuest.Contracts;
using LeedsBeerQuest.CustomExceptions;
using LeedsBeerQuest.Mapping;
using LeedsBeerQuest.Repositories;
using LeedsBeerQuest.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeedsBeerQuest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeedsBeerQuestController : ControllerBase
    {
        private readonly ILogger<LeedsBeerQuestController> _logger;
        private readonly IReviewService _reviewService;

        public LeedsBeerQuestController(
            ILogger<LeedsBeerQuestController> logger,
            IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        [HttpGet("GetReview{name}")]
        public IActionResult GetReview(string name)
        {
            try
            {
                return Ok(_reviewService.GetReview(name));
            }
            catch (ExpectedDataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAllReviews")]
        public IEnumerable<ReviewClientDTO> GetAllReviews()
        {
            return _reviewService.GetAllReviews().Select(r => DTOMapper.ConvertToClient(r));
        }

        [HttpGet("GetFilteredReviews")]
        public IEnumerable<ReviewClientDTO> GetFilteredReviews([FromQuery] ReviewFilters reviewFilters)
        {
            return _reviewService.GetFilteredReviewSourceDTOs(reviewFilters).Select(r => DTOMapper.ConvertToClient(r));
        }


        [HttpPost("CreateReview")]
        public IActionResult CreateReview(ReviewClientDTO review)
        {
            try
            {
                _reviewService.CreateReview(DTOMapper.ConvertFromClient(review));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteReview")]
        public IActionResult DeleteReview(string name)
        {
            _reviewService.DeleteReview(name);
            return Ok();
        }
    }
}