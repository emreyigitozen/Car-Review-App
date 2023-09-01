using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategories();
        Category GetCategory(int id);
        bool CategoryExists(int catid);
        ICollection<Car> GetCarByCategory(int catid);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();

    }
}
