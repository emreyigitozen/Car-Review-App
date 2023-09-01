using AutoMapper;
using CarReviewApp.Dto;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetAllCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetAllCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }
        [HttpGet("{catid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int catid) {
        if(!_categoryRepository.CategoryExists(catid))
            {
                return NotFound();
            }
        var category=_mapper.Map<CategoryDto>(_categoryRepository.GetCategory(catid));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }
        [HttpGet("car/{carid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetCarByCategory(int carid)
        {
            var car=_mapper.Map<List<CarDto>>(_categoryRepository.GetCarByCategory(carid));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(car);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody]CategoryDto categorycreate) {
         if(categorycreate == null)
            {
                return BadRequest(ModelState);
            }
            var category = _categoryRepository.GetAllCategories().Where(c => c.Name.Trim().ToUpper() == categorycreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if(category != null)
            {
                ModelState.AddModelError("", "Category exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          var categorymap=_mapper.Map<Category>(categorycreate);
            if (!_categoryRepository.CreateCategory(categorymap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500,ModelState);
            }
            return Ok("Created without any problem");

        }
        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody]CategoryDto updatedcategory) {
         if(updatedcategory == null)
            {
                return BadRequest(ModelState);
            }
         if(categoryId != updatedcategory.Id)
            {
                return BadRequest(ModelState);
            }
         if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
         if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categorymap = _mapper.Map<Category>(updatedcategory);
            if (!_categoryRepository.CreateCategory(categorymap))
            {
                ModelState.AddModelError("", "Smth is wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        
        }
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var categoryToDelete = _categoryRepository.GetCategory(categoryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }




    } 
}
