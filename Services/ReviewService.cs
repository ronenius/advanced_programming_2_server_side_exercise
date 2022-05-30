using advanced_programming_2_server_side_exercise.Data;
using advanced_programming_2_server_side_exercise.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_programming_2_server_side_exercise.Services
{
    public class ReviewService : IReviewService
    {
        private readonly advanced_programming_2_server_side_exerciseContext _context;

        public ReviewService(advanced_programming_2_server_side_exerciseContext context)
        {
            _context = context;
        }

        public async Task Create(int score, string feedback, string name, DateTime date)
        {
            Review newReview = new Review();
            newReview.Score = score;
            newReview.Feedback = feedback;
            newReview.Name = name;
            newReview.DateTime = date;
            _context.Review.Add(newReview);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Review review = await _context.Review.FirstOrDefaultAsync(rev => rev.Id == id);
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Review review)
        {
            _context.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> Get(int id)
        {
            return await _context.Review.FirstOrDefaultAsync(rev => rev.Id == id);
        }

        public async Task<List<Review>> GetAll()
        {
            return await _context.Review.ToListAsync();
        }

        public async Task<bool> isIdExist(int id)
        {
            return await _context.Review.AnyAsync(e => e.Id == id);
        }
    }
}
