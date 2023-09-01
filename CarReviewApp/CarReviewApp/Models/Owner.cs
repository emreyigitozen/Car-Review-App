namespace CarReviewApp.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CarOwner> CarOwners { get; set; }

    }
}
