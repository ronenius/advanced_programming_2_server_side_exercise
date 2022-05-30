using advanced_programming_2_server_side_exercise.Data;
using advanced_programming_2_server_side_exercise.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_programming_2_server_side_exercise.Services
{
    public class UserService : IUserService
    {
        private readonly advanced_programming_2_server_side_exerciseContext _context;

        public UserService(advanced_programming_2_server_side_exerciseContext context)
        {
            _context = context;
        }

        public async Task Create(string username, string password, string name)
        {
            User newUser = new User();
            newUser.Username = username;
            newUser.Password = password;
            newUser.Name = name;
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Get(string username)
        {
            return await _context.User.FirstOrDefaultAsync(user => user.Username == username);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        public async Task Delete(string username)
        {
            User user = await _context.User.FirstOrDefaultAsync(usr => usr.Username == username);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> isIdExist(string username)
        {
            return await _context.User.AnyAsync(usr => usr.Username == username);
        }
    }
}
