using AutoMapper;
using CarReviewApp.Dto;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public CarController(ICarRepository car,IMapper mapper)
        {
            _carRepository = car;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetAllCars()
        {
            var cars = _mapper.Map<List<CarDto>>(_carRepository.GetAllCars());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(cars);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Car))]
        [ProducesResponseType(400)]
        public IActionResult GetCar(int id)
        {
            if (!_carRepository.CarExists(id))
            {
                return NotFound();
            }
            var car=_mapper.Map<CarDto>(_carRepository.GetCar(id));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(car);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCar([FromQuery] int ownerid, [FromQuery]int catid, [FromBody] CarDto carcreate )
        {
            if (carcreate == null)
            {
                return BadRequest();
            }
            var cars=_carRepository.GetAllCars().Where(c=>c.Name.Trim().ToUpper()==carcreate.Name.Trim().ToUpper()).FirstOrDefault();
            if(cars != null)
            {
                ModelState.AddModelError("","This one already exists");
                return StatusCode(422, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var carmap = _mapper.Map<Car>(carcreate);
            if (!_carRepository.CreateCar(ownerid,catid,carmap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500,ModelState);
            }
            return Ok("Created without any problem");
        }
        [HttpPut("{carId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCar(int carId, [FromQuery]int ownerId, [FromQuery] int catId, [FromBody]CarDto updatedcar)
        {
            if(updatedcar == null)
            {
                return BadRequest(ModelState);
            }
            if(carId!=updatedcar.Id)
            {
                return BadRequest(ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_carRepository.CarExists(carId))
            {
                return NotFound();
            }
            var carmap = _mapper.Map<Car>(updatedcar);
            if (!_carRepository.UpdateCar(ownerId, catId, carmap))
            {
                ModelState.AddModelError("", "Smth went wrong");
                return StatusCode(500,ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{carId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCar(int carId)
        {
            if (!_carRepository.CarExists(carId))
            {
                return NotFound();
            }
            var carsToDelete=_carRepository.GetCar(carId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_carRepository.DeleteCar(carsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
