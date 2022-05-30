using advanced_programming_2_server_side_exercise.Models;

namespace advanced_programming_2_server_side_exercise.Services
{
    public interface IReviewService
    {
        public Task<List<Review>> GetAll();

        public Task<Review> Get(int id);

        public Task Create(int score, string feedback, string name, DateTime date);

        public Task Delete(int id);

        public Task Edit(Review review);

        public Task<bool> isIdExist(int id);
    }
}
