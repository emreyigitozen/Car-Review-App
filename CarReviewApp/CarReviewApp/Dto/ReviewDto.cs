using CarReviewApp.Models;

namespace CarReviewApp.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Reviewer Reviewer { get; set; }
        public Car Car { get; set; }
    }
}
