using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetAllOwners();
        Owner GetOwner(int id);
        bool OwnerExists(int id);
        ICollection<Owner> GetOwnerOfCar(int carid);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(Owner owner);
        bool Save();
    }
}
