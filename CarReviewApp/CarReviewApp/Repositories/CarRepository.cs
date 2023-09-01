using CarReviewApp.Data;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;

namespace CarReviewApp.Repositories
{

   
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;

        public CarRepository(DataContext dataContext)
        {
            _context = dataContext;
        }



        public bool CarExists(int carid)
        {
            return _context.Cars.Any();
        }

        public ICollection<Car> GetAllCars()
        {
            return _context.Cars.ToList();
        }

        public Car GetCar(int id)
        {
            return _context.Cars.Where(c => c.Id == id).FirstOrDefault();
        }
        public bool CreateCar(int ownerid, int categoryid, Car car)
        {
            var carownerentity=_context.Owners.Where(o=>o.Id==ownerid).FirstOrDefault();
            var categoryentity=_context.Categories.Where(c=>c.Id==categoryid).FirstOrDefault();
            var carowner = new CarOwner()
            {
                Owner = carownerentity,
                Car = car

            };
            _context.Add(carowner);
            var carcategory = new CarCategory()
            {
                Category = categoryentity,
                Car = car
            };
            _context.Add(carcategory);
            _context.Add(car);
            return Save();
        }

        public bool DeleteCar(Car car)
        {
            _context.Remove(car);
            return Save();
        }
        public bool UpdateCar(int ownerid, int categoryid, Car car)
        {
            _context.Update(car);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0? true : false;
        }
    }
}
