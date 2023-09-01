using AutoMapper;
using CarReviewApp.Dto;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _mapper = mapper;
            _ownerRepository = ownerRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetAllOwners()
        {
            var owners=_mapper.Map<List<OwnerDto>>(_ownerRepository.GetAllOwners());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owners);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int id)
        {
           if(!_ownerRepository.OwnerExists(id))
            {
                return NotFound();
            }
           var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(id));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }
        [HttpGet("car/{ownerid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerOfCar(int ownerid)
        {
            if(!_ownerRepository.OwnerExists(ownerid)) 
            {
                return NotFound();
            }
            var ownerofcar=_mapper.Map<OwnerDto>(_ownerRepository.GetOwnerOfCar(ownerid));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(ownerofcar);
                        
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromBody] OwnerDto ownercreate)
        {
            if (ownercreate == null)
            {
                return BadRequest(ModelState);
            }
            var owner=_ownerRepository.GetAllOwners().Where(o=>o.Name.Trim().ToUpper()==ownercreate.Name.Trim().ToUpper()).FirstOrDefault();
            if (owner != null)
            {
                ModelState.AddModelError("", "This one already exists");
                return StatusCode(422,ModelState);
            }
            var ownermap = _mapper.Map<Owner>(ownercreate);
            if (!_ownerRepository.CreateOwner(ownermap))
            {
                ModelState.AddModelError("", "Something is not right");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfuly created");
        }
        [HttpPut("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto updatedowner)
        {
            if (updatedowner == null)
            {
                return BadRequest(ModelState);
            }
            if(ownerId!=updatedowner.Id)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }
            var ownermap = _mapper.Map<Owner>(updatedowner);
            if (!_ownerRepository.CreateOwner(ownermap))
            {
                ModelState.AddModelError("", "Something is wrong");
                return StatusCode(500,ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }
            var ownerToDelete=_ownerRepository.GetOwner(ownerId);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_ownerRepository.DeleteOwner(ownerToDelete))
            {
                ModelState.AddModelError("", "Smth went wrong while deleting");
                return StatusCode(500,ModelState);
            }
            return NoContent();
        }
    }
}
