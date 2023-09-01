using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car> GetAllCars();
        Car GetCar(int id);
        bool CarExists(int carid);

        bool CreateCar(int ownerid,int categoryid,Car car);
        bool DeleteCar(Car car);
        bool UpdateCar(int ownerid,int categoryid, Car car);
        bool Save();





    }
}
