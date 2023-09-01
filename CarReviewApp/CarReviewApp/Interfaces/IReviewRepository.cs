using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetAllReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfReviewer(int reviewerid);
        bool ReviewExists(int id);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);

        bool Save();

    }
}
