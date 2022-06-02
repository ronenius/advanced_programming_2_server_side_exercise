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
using advanced_programming_2_server_side_exercise.Hubs;
using advanced_programming_2_server_side_exercise.APIObjects;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR;

namespace advanced_programming_2_server_side_exercise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMessageService _messageService;
        private readonly IHubContext<MyHub, IMyHub> _myHub;

        public TransferController(advanced_programming_2_server_side_exerciseContext context, IHubContext<MyHub, IMyHub> myHub)
        {
            _contactService = new ContactService(context);
            _messageService = new MessageService(context);
            _myHub = myHub;
        }

        // POST: api/transfer
        [HttpPost]
        public async Task<IActionResult> Post([Bind("From,To,Content")] NewTransfer newTransfer)
        {
            List<Contact> contacts = await _contactService.GetAll();
            foreach (Contact contact in contacts)
            {
                if (contact.Id == newTransfer.To + ";" + newTransfer.From)
                {
                    await _messageService.Create(newTransfer.From, newTransfer.To, newTransfer.Content, DateTime.Now);
                    await _myHub.Clients.All.NewMessage();
                    return Created("/api/contacts/" + newTransfer.From + "/messages", new MessageAPI(null, newTransfer.Content, false, DateTime.Now));
                }
            }
            return NotFound();
        }
    }
}