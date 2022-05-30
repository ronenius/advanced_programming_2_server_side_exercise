using advanced_programming_2_server_side_exercise.Models;

namespace advanced_programming_2_server_side_exercise.Services
{
    public interface IContactService
    {
        public Task<List<Contact>> GetAll();

        public Task<Contact> Get(string id);

        public Task<Contact> Get(string username, string contactUsername);

        public Task Create(string username, string contactUsername, string contactServer, string name);

        public Task Delete(string id);

        public Task Delete(string username, string contactUsername);

        public Task Edit(Contact contact);

        public Task<bool> isIdExist(string id);

        public Task<bool> isIdExist(string username, string contactUsername);
    }
}
