using AutoMapper;
using CarReviewApp.Dto;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetAllReviews()
        {
            var reviews=_mapper.Map<List<ReviewDto>>(_reviewRepository.GetAllReviews());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int id)
        {
            if (!_reviewRepository.ReviewExists(id))
            {
                return NotFound();
            }
            var review=_mapper.Map<OwnerDto>(_reviewRepository.GetReview(id));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }
        [HttpGet("reviewer/{reviewerid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfReviewer(int reviewerid)
        {
            if (!_reviewRepository.ReviewExists(reviewerid))
            {
                return NotFound();
            }
            var reviews=_mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfReviewer(reviewerid));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromBody] ReviewDto reviewcreate)
        {
            if (reviewcreate == null)
            {
                return BadRequest(ModelState);
            }
            var review=_reviewRepository.GetAllReviews().Where(r=>r.Title.Trim().ToUpper()==reviewcreate.Title.Trim().ToUpper()).FirstOrDefault();
            if(review != null)
            {
                ModelState.AddModelError("", "This one already exists");
                return StatusCode(422,ModelState);
            }
            var reviewmap = _mapper.Map<Review>(reviewcreate);
            if (!_reviewRepository.CreateReview(reviewmap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfuly created");

        }
        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewid, [FromBody] ReviewDto updatedreview) {
        
          if(updatedreview == null)
            {
                return BadRequest(ModelState);
            }
          if(reviewid!=updatedreview.Id)
            {
                return BadRequest(ModelState);
            }
            if (_reviewRepository.ReviewExists(reviewid))
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewmap = _mapper.Map<Review>(updatedreview);
            if (_reviewRepository.CreateReview(reviewmap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500,ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewid)
        {
            if (!_reviewRepository.ReviewExists(reviewid))
            {
                return NotFound();
            }
            var reviewToDelete=_reviewRepository.GetReview(reviewid);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Smth went wrong while deleting");
                return StatusCode(500,ModelState);
            }
            return NoContent();
        }


    }
}
