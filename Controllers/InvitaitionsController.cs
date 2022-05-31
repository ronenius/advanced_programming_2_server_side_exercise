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
    public class InvitationsController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IUserService _userService;
        private readonly string _server;

        public InvitationsController(advanced_programming_2_server_side_exerciseContext context)
        {
            _contactService = new ContactService(context);
            _userService = new UserService(context);
            _server = Request.Host.Host + ":" + Request.Host.Port;
        }

        // POST: api/invitations
        [HttpPost]
        public async Task<IActionResult> Post(string from, string to, string server)
        {
            List<User> users = await _userService.GetAll();
            foreach (User user in users)
            {
                if (to == user.Username)
                {
                    await _contactService.Create(to, from, server, from);
                    return Created(_server + "/api/contacts/" + from, new ContactAPI(to, from, server, null, null));
                }
            }
            return NotFound();
        }
    }
}