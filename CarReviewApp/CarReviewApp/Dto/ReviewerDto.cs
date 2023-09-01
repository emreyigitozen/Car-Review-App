using CarReviewApp.Models;

namespace CarReviewApp.Dto
{
    public class ReviewerDto
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
