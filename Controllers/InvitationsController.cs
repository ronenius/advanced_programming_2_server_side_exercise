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

namespace advanced_programming_2_server_side_exercise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationsController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IUserService _userService;

        public InvitationsController(advanced_programming_2_server_side_exerciseContext context)
        {
            _contactService = new ContactService(context);
            _userService = new UserService(context);
        }

        // POST: api/invitations
        [HttpPost]
        public async Task<IActionResult> Post([Bind("From,To,Server")] NewInvitation newInvitation)
        {
            Contact contact = await _contactService.Get(newInvitation.To, newInvitation.From);
            if (contact != null)
            {
                return Conflict();
            }
            List<User> users = await _userService.GetAll();
            foreach (User user in users)
            {
                if (newInvitation.To == user.Username)
                {
                    await _contactService.Create(newInvitation.To, newInvitation.From, newInvitation.Server, newInvitation.From);
                    /*var connection = new HubConnection("/myHub");
                    var myHub = connection.CreateHubProxy("MyHub");
                    await connection.Start();
                    await myHub.Invoke("NewContact");
                    connection.Stop();*/
                    return Created("/api/contacts/" + newInvitation.From, new ContactAPI(newInvitation.To, newInvitation.From, newInvitation.Server, null, null));
                }
            }
            return NotFound();
        }
    }
}