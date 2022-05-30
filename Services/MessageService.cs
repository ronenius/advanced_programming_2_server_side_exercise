using advanced_programming_2_server_side_exercise.Data;
using advanced_programming_2_server_side_exercise.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_programming_2_server_side_exercise.Services
{
    public class MessageService : IMessageService
    {
        private readonly advanced_programming_2_server_side_exerciseContext _context;

        public MessageService(advanced_programming_2_server_side_exerciseContext context)
        {
            _context = context;
        }

        public async Task Create(string fromUser, string toUser, string content, DateTime created)
        {
            Message newMessage = new Message();
            newMessage.FromUsername = fromUser;
            newMessage.ToUsername = toUser;
            newMessage.Content = content;
            newMessage.Created = created;
            _context.Message.Add(newMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> Get(int id)
        {
            return await _context.Message.FirstOrDefaultAsync(msg => msg.Id == id);
        }

        public async Task<List<Message>> GetAll()
        {
            return await _context.Message.ToListAsync();
        }

        public async Task Delete(int id)
        {
            Message message = await _context.Message.FirstOrDefaultAsync(msg => msg.Id == id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Message message)
        {
            _context.Update(message);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> isIdExist(int id)
        {
            return await _context.Message.AnyAsync(msg => msg.Id == id);
        }
    }
}
