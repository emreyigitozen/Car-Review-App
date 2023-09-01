using System.Collections;

namespace CarReviewApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<CarCategory> CarCategories { get; set; }


    }
}
