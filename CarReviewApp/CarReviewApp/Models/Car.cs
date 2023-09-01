namespace CarReviewApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CarOwner> CarOwners { get; set; }

        public ICollection<CarCategory> CarCategories { get; set; }
        public ICollection<Review> Review { get; set; }

    }
}
