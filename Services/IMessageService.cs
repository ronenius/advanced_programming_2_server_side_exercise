using advanced_programming_2_server_side_exercise.Models;

namespace advanced_programming_2_server_side_exercise.Services
{
    public interface IMessageService
    {
        public Task<List<Message>> GetAll();

        public Task<Message> Get(int id);

        public Task Create(string fromUser, string toUser, string content, DateTime created);

        public Task Delete(int id);

        public Task Edit(Message message);

        public Task<bool> isIdExist(int id);
    }
}
