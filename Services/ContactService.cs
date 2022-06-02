using advanced_programming_2_server_side_exercise.Data;
using advanced_programming_2_server_side_exercise.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_programming_2_server_side_exercise.Services
{
    public class ContactService : IContactService
    {
        private readonly advanced_programming_2_server_side_exerciseContext _context;

        public ContactService(advanced_programming_2_server_side_exerciseContext context)
        {
            _context = context;
        }

        public async Task Create(string username, string contactUsername, string contactServer, string contactName)
        {
            Contact newContact = new Contact();
            newContact.Id = username + ";" + contactUsername;
            newContact.ContactServer = contactServer;
            newContact.ContactNickname = contactName;
            newContact.Messages = new List<Message>();
            _context.Contact.Add(newContact);
            await _context.SaveChangesAsync();
        }

        public async Task<Contact> Get(string id)
        {
            return await _context.Contact.FirstOrDefaultAsync(cont => cont.Id == id);
        }

        public async Task<Contact> Get(string username, string contactUsername)
        {
            string id = username + ";" + contactUsername;
            return await _context.Contact.FirstOrDefaultAsync(cont => cont.Id == id);
        }

        public async Task<List<Contact>> GetAll()
        {
            return await _context.Contact.ToListAsync();
        }

        public async Task Delete(string id)
        {
            Contact contact = await _context.Contact.FirstOrDefaultAsync(cont => cont.Id == id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string username, string contactUsername)
        {
            Contact contact = await _context.Contact.FirstOrDefaultAsync(cont => cont.Id == username + ";" + contactUsername);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Contact contact)
        {
            _context.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> isIdExist(string id)
        {
            return await _context.Contact.AnyAsync(cont => cont.Id == id);
        }

        public async Task<bool> isIdExist(string username, string contactUsername)
        {
            return await _context.Contact.AnyAsync(cont => cont.Id == username + ";" + contactUsername);
        }
    }
}
