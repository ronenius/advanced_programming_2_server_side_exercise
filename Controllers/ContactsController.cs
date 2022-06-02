using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_programming_2_server_side_exercise.Data;
using advanced_programming_2_server_side_exercise.Models;
using advanced_programming_2_server_side_exercise.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting.Server;
using advanced_programming_2_server_side_exercise.Hubs;
using advanced_programming_2_server_side_exercise.APIObjects;
using Microsoft.AspNet.SignalR.Client;

namespace advanced_programming_2_server_side_exercise.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMessageService _messageService;

        public ContactsController(advanced_programming_2_server_side_exerciseContext context)
        {
            _contactService = new ContactService(context);
            _messageService = new MessageService(context);
            /*List<Claim> claims = ((ClaimsIdentity)HttpContext.User.Identity).Claims.ToList();
            foreach (Claim claim in claims)
            {
                if (claim.Type == "UserId")
                {
                    _username = claim.Value;
                    break;
                }
            }
            _server = Request.Host.Host + ":" + Request.Host.Port;*/
        }

        // GET: api/Contacts/username
        [HttpGet]
        public async Task<IActionResult> Get(string user)
        {
            List<Contact> contacts = await _contactService.GetAll();
            List<ContactAPI> contactsApi = new List<ContactAPI>();
            foreach (Contact contact in contacts)
            {
                string[] usernames = contact.Id.Split(';');
                if (user == usernames[0])
                {
                    List<Message> messages = await _messageService.GetAll();
                    List<MessageAPI> contactMessages = new List<MessageAPI>();
                    foreach (Message message in messages)
                    {
                        if ((user == message.FromUsername && usernames[1] == message.ToUsername) || (user == message.ToUsername && usernames[1] == message.FromUsername))
                        {
                            if (user == message.FromUsername)
                            {
                                contactMessages.Add(new MessageAPI(message.Id, message.Content, true, message.Created));
                            }
                            else
                            {
                                contactMessages.Add(new MessageAPI(message.Id, message.Content, false, message.Created));
                            }
                        }
                    }
                    string lastMessage = null;
                    DateTime? lastDate = null;
                    if (contactMessages.Count() > 0)
                    {
                        lastMessage = contactMessages[contactMessages.Count() - 1].Content;
                        lastDate = contactMessages[contactMessages.Count() - 1].Created;
                    }
                    contactsApi.Add(new ContactAPI(usernames[1], contact.ContactNickname, contact.ContactServer, lastMessage, lastDate));
                }
            }
            return Json(contactsApi);
        }

        // GET: api/Contacts/username/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(string id, string user)
        {
            List<Contact> contacts = await _contactService.GetAll();
            foreach (Contact contact in contacts)
            {
                string[] usernames = contact.Id.Split(';');
                if (user == usernames[0])
                {
                    if (id == usernames[1])
                    {
                        List<Message> messages = await _messageService.GetAll();
                        List<MessageAPI> contactMessages = new List<MessageAPI>();
                        foreach (Message message in messages)
                        {
                            if ((user == message.FromUsername && usernames[1] == message.ToUsername) || (user == message.ToUsername && usernames[1] == message.FromUsername))
                            {
                                if (user == message.FromUsername)
                                {
                                    contactMessages.Add(new MessageAPI(message.Id, message.Content, true, message.Created));
                                }
                                else
                                {
                                    contactMessages.Add(new MessageAPI(message.Id, message.Content, false, message.Created));
                                }
                            }
                        }
                        string lastMessage = null;
                        DateTime? lastDate = null;
                        if (contactMessages.Count() > 0)
                        {
                            lastMessage = contactMessages[contactMessages.Count() - 1].Content;
                            lastDate = contactMessages[contactMessages.Count() - 1].Created;
                        }
                        return Json(new ContactAPI(usernames[1], contact.ContactNickname, contact.ContactServer, lastMessage, lastDate));
                    }
                }
            }
            return NotFound();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<IActionResult> Post([Bind("Id,Name,Server,User")] NewContact newcontact)
        {
            Contact contact = await _contactService.Get(newcontact.User, newcontact.Id);
            if (contact != null)
            {
                return Conflict();
            }
            await _contactService.Create(newcontact.User, newcontact.Id, newcontact.Server, newcontact.Name);
            /*var connection = new HubConnection("/myHub");
            var myHub = connection.CreateHubProxy("MyHub");
            await connection.Start();
            await myHub.Invoke("NewContact");
            connection.Stop();*/
            return Created("/api/contacts/" + newcontact.Id, new ContactAPI(newcontact.Id, newcontact.Name, newcontact.Server, null, null));

        }

        // DELETE: api/Contacts/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id, [Bind("User")] NewContact newcontact)
        {
            var contact = await _contactService.Get(newcontact.User, id);
            if (contact == null)
            {
                return NotFound();
            }
            await _contactService.Delete(id);
            /*var connection = new HubConnection("/myHub");
            var myHub = connection.CreateHubProxy("MyHub");
            await connection.Start();
            await myHub.Invoke("NewContact");
            connection.Stop();*/
            return NoContent();
        }

        // PUT: api/Contacts/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Server,User")] NewContact newcontact)
        {
            var contact = await _contactService.Get(newcontact.User, id);
            if (contact == null)
            {
                return NotFound();
            }
            contact.ContactNickname = newcontact.Name;
            contact.ContactServer = newcontact.Server;
            await _contactService.Edit(contact);
            /*var connection = new HubConnection("/myHub");
            var myHub = connection.CreateHubProxy("MyHub");
            await connection.Start();
            await myHub.Invoke("NewContact");
            connection.Stop();*/
            return NoContent();
        }

        // GET: api/contacts/id/messages
        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetMessages(string id, string user)
        {
            List<Message> messages = await _messageService.GetAll();
            List<MessageAPI> messagesAPI = new List<MessageAPI>();
            foreach (Message message in messages)
            {
                if ((user == message.FromUsername && id == message.ToUsername) || (user == message.ToUsername && id == message.FromUsername))
                {
                    if (user == message.FromUsername)
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
        public async Task<IActionResult> GetMessage(string id, int id2, string user)
        {
            Message message = await _messageService.Get(id2);
            if (message == null)
            {
                return NotFound();
            }
            if (user == message.FromUsername)
            {
                return Json(new MessageAPI(id2, message.Content, true, message.Created));
            }
            return Json(new MessageAPI(id2, message.Content, false, message.Created));
        }

        // POST: api/contacts/id/messages
        [HttpPost("{id}/messages")]
        public async Task<IActionResult> PostMessage(string id, [Bind("User,Content")] NewMessage newMessage)
        {
            await _messageService.Create(newMessage.User, id, newMessage.Content, DateTime.Now);
            /*var connection = new HubConnection("http://localhost:5178/");
            var myHub = connection.CreateHubProxy("MyHub");
            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();
            myHub.Invoke<string>("NewMessage").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error calling send: {0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            }).Wait();
            connection.Stop();*/
            //MyHub myHub = new MyHub();
            //await myHub.NewMessage();
            return Created("/api/contacts/" + id + "/messages", new MessageAPI(null, newMessage.Content, true, DateTime.Now));
        }

        // DELETE: api/contacts/id/messages/id2
        [HttpDelete("{id}/messages/{id2}")]
        public async Task<IActionResult> DeleteMessage(string id, int id2, [Bind("User")] NewMessage newMessage)
        {
            Message message = await _messageService.Get(id2);
            if (message == null)
            {
                return NotFound();
            }
            await _messageService.Delete(id2);
            /*var connection = new HubConnection("/myHub");
            var myHub = connection.CreateHubProxy("MyHub");
            await connection.Start();
            await myHub.Invoke("NewMessage");
            connection.Stop();*/
            return NoContent();
        }

        // PUT: api/contacts/id/messages/id2
        [HttpPut("{id}/messages/{id2}")]
        public async Task<IActionResult> EditMessage(string id, int id2, [Bind("User,Content")] NewMessage newMessage)
        {
            Message message = await _messageService.Get(id2);
            if (message == null)
            {
                return NotFound();
            }
            message.Content = newMessage.Content;
            await _messageService.Edit(message);
            /*var connection = new HubConnection("/myHub");
            var myHub = connection.CreateHubProxy("MyHub");
            await connection.Start();
            await myHub.Invoke("NewMessage");
            connection.Stop();*/
            return NoContent();
        }
    }
}
