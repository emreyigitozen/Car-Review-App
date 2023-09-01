using CarReviewApp.Data;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;

namespace CarReviewApp.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        public OwnerRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return Save();
        }

        public ICollection<Owner> GetAllOwners()
        {
            return _context.Owners.ToList();
        }

        public Owner GetOwner(int id)
        {
            return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfCar(int carid)
        {
            return _context.CarOwners.Where(c=>c.Car.Id == carid).Select(o=>o.Owner).ToList();
        }

        public bool OwnerExists(int id)
        {
            return _context.Owners.Any();
        }

        public bool Save()
        {
            var saved= _context.SaveChanges();
            return saved>0? true: false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }
    }
}
