using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_programming_2_server_side_exercise.Data;
using advanced_programming_2_server_side_exercise.Models;
using advanced_programming_2_server_side_exercise.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting.Server;

namespace advanced_programming_2_server_side_exercise.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMessageService _messageService;
        private readonly IConfiguration _configuration;
        private readonly string _username;
        private readonly string _server;

        public ContactsController(advanced_programming_2_server_side_exerciseContext context, IConfiguration config)
        {
            _contactService = new ContactService(context);
            _messageService = new MessageService(context);
            _configuration = config;
            List<Claim> claims = ((ClaimsIdentity)HttpContext.User.Identity).Claims.ToList();
            foreach (Claim claim in claims)
            {
                if (claim.Type == "UserId")
                {
                    _username = claim.Value;
                    break;
                }
            }
            _server = Request.Host.Host + ":" + Request.Host.Port;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Contact> contacts = await _contactService.GetAll();
            List<ContactAPI> contactsApi = new List<ContactAPI>();
            foreach (Contact contact in contacts)
            {
                string[] usernames = contact.Id.Split(';');
                if (_username == usernames[0])
                {
                    List<Message> messages = contact.Messages;
                    string lastMessage = null;
                    DateTime? lastDate = null;
                    if (messages.Count > 0)
                    {
                        lastMessage = messages[messages.Count - 1].Content;
                        lastDate = messages[messages.Count - 1].Created;
                    }
                    contactsApi.Add(new ContactAPI(usernames[1], contact.ContactNickname, contact.ContactServer, lastMessage, lastDate));
                }
            }
            return Json(contactsApi);
        }

        // GET: api/Contacts/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(string id)
        {
            List<Contact> contacts = await _contactService.GetAll();
            foreach (Contact contact in contacts)
            {
                string[] usernames = contact.Id.Split(';');
                if (_username == usernames[0])
                {
                    if (id == usernames[1])
                    {
                        List<Message> messages = contact.Messages;
                        string lastMessage = null;
                        DateTime? lastDate = null;
                        if (messages.Count > 0)
                        {
                            lastMessage = messages[messages.Count - 1].Content;
                            lastDate = messages[messages.Count - 1].Created;
                        }
                        return Json(new ContactAPI(usernames[1], contact.ContactNickname, contact.ContactServer, lastMessage, lastDate));
                    }
                }
            }
            return NotFound();
        }

        // POST: api/Contacts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(string id, string name, string server)
        {
            Contact contact = await _contactService.Get(_username, id);
            if (contact != null)
            {
                return Conflict();
            }
            await _contactService.Create(_username, id, server, name);
            return Created(_server + "/api/contacts/" + id, new ContactAPI(id, name, server, null, null));

        }

        // DELETE: api/Contacts/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            var contact = await _contactService.Get(_username, id);
            if (contact == null)
            {
                return NotFound();
            }
            await _contactService.Delete(id);
            return NoContent();
        }

        // PUT: api/Contacts/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, string name, string server)
        {
            var contact = await _contactService.Get(_username, id);
            if (contact == null)
            {
                return NotFound();
            }
            contact.ContactNickname = name;
            contact.ContactServer = server;
            await _contactService.Edit(contact);
            return NoContent();
        }

        // GET: api/contacts/id/messages
        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetMessages(string id)
        {
            List<Message> messages = await _messageService.GetAll();
            List<MessageAPI> messagesAPI = new List<MessageAPI>();
            foreach (Message message in messages)
            {
                if ((_username == message.FromUsername && id == message.ToUsername) || (_username == message.ToUsername && id == message.FromUsername))
                {
                    if (_username == message.FromUsername)
                    {
                        messagesAPI.Add(new MessageAPI(message.Id, message.Content, true, message.Created));
                    }
                    else
                    {
                        messagesAPI.Add(new MessageAPI(message.Id, message.Content, false, message.Created));
                    }
                }
            }
            return Json(messagesAPI);
        }

        // GET: api/contacts/id/messages/id2
        [HttpGet("{id}/messages/{id2}")]
        public async Task<IActionResult> GetMessage(string id, int id2)
        {
            Message message = await _messageService.Get(id2);
            if (message == null)
            {
                return NotFound();
            }
            if (_username == message.FromUsername)
            {
                return Json(new MessageAPI(id2, message.Content, true, message.Created));
            }
            return Json(new MessageAPI(id2, message.Content, false, message.Created));
        }

        // POST: api/contacts/id/messages
        [HttpPost("{id}/messages")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostMessage(string id, string content)
        {
            await _messageService.Create(_username, id, content, DateTime.Now);
            return Created(_server + "/api/contacts/" + id + "/messages", new MessageAPI(null, content, true, DateTime.Now));
        }

        // DELETE: api/contacts/id/messages/id2
        [HttpDelete("{id}/messages/{id2}")]
        public async Task<IActionResult> DeleteMessage(string id, int id2)
        {
            Message message = await _messageService.Get(id2);
            if (message == null)
            {
                return NotFound();
            }
            await _messageService.Delete(id2);
            return NoContent();
        }

        // PUT: api/contacts/id/messages/id2
        [HttpPut("{id}/messages/{id2}")]
        public async Task<IActionResult> EditMessage(string id, int id2, string content)
        {
            Message message = await _messageService.Get(id2);
            if (message == null)
            {
                return NotFound();
            }
            message.Content = content;
            await _messageService.Edit(message);
            return NoContent();
        }
    }
}
