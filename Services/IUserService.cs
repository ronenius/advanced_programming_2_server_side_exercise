using advanced_programming_2_server_side_exercise.Models;

namespace advanced_programming_2_server_side_exercise.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAll();

        public Task<User> Get(string username);

        public Task Create(string username, string password, string name);

        public Task Delete(string username);

        public Task Edit(User user);

        public Task<bool> isIdExist(string username);
    }
}
