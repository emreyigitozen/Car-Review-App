using CarReviewApp.Data;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CarReviewApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext dataContext)
        {
            _context = dataContext;
        }


        public bool CategoryExists(int catid)
        {
            return _context.Categories.Any();
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
             return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public ICollection<Car> GetCarByCategory(int catid)
        {
            return _context.CarCategories.Where(c=>c.Category.Id== catid).Select(c=>c.Car).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c=>c.Id==id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved=_context.SaveChanges();
            return saved >0? true: false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
