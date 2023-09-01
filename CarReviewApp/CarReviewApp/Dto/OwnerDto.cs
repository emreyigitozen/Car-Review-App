using CarReviewApp.Models;

namespace CarReviewApp.Dto
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CarOwner> CarOwners { get; set; }
    }
}
