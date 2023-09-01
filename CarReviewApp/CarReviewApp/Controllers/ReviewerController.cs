using AutoMapper;
using CarReviewApp.Dto;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;
        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetAllReviewers()
        {
            var reviewers=_mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetAllReviewers());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewers);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetReviewer(int id)
        {
            if(!_reviewerRepository.ReviewerExists(id))
            {
                return NotFound();
            }
            var reviewer=_mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(id));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewer);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody]ReviewerDto reviewercreate)
        {
            if (reviewercreate == null)
            {
                return BadRequest(ModelState);
            }
            var reviewer=_reviewerRepository.GetAllReviewers().Where(r=>r.Name.Trim().ToUpper()==reviewercreate.Name.Trim().ToUpper()).FirstOrDefault();
            if(reviewer != null)
            {
                ModelState.AddModelError("","This reviewer already exists");
                return StatusCode(422, ModelState);
            }
            var reviewermap = _mapper.Map<Reviewer>(reviewercreate);
            if (!_reviewerRepository.CreateReviewer(reviewermap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500,ModelState);
            }
            return Ok("Created");
        }
        [HttpPut("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody]ReviewerDto updatedreviewer)
        {
            if(updatedreviewer == null)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(reviewerId!=updatedreviewer.Id)
            {
                return BadRequest(ModelState);
            }
            var reviewermap = _mapper.Map<Reviewer>(updatedreviewer);
            if (!_reviewerRepository.UpdateReviewer(reviewermap))
            {
                ModelState.AddModelError("", "Something is wrong");
                return StatusCode(500,ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            var reviewerToDelete = _reviewerRepository.GetReviewer(reviewerId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewerRepository.DeleteReviewer(reviewerToDelete))
            {
                ModelState.AddModelError("", "Smth went wrong");
                return StatusCode(500,ModelState);
            }
            return NoContent();
                    
        }

    }
}
