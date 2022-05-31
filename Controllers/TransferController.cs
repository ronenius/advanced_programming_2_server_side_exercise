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
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMessageService _messageService;
        private readonly string _server;

        public TransferController(advanced_programming_2_server_side_exerciseContext context)
        {
            _contactService = new ContactService(context);
            _messageService = new MessageService(context);
            _server = Request.Host.Host + ":" + Request.Host.Port;
        }

        // POST: api/transfer
        [HttpPost]
        public async Task<IActionResult> Post(string from, string to, string content)
        {
            List<Contact> contacts = await _contactService.GetAll();
            foreach (Contact contact in contacts)
            {
                if (contact.Id == to + ";" + from)
                {
                    await _messageService.Create(from, to, content, DateTime.Now);
                    return Created(_server + "/api/contacts/" + from + "/messages", new MessageAPI(null, content, false, DateTime.Now));
                }
            }
            return NotFound();
        }
    }
}