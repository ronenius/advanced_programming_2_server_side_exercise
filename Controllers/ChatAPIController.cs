using advanced_programming_2_server_side_exercise.Data;
using advanced_programming_2_server_side_exercise.Models;
using Microsoft.AspNetCore.Mvc;

namespace advanced_programming_2_server_side_exercise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatAPIController : Controller
    {

        private readonly advanced_programming_2_server_side_exerciseContext _context;

        public ChatAPIController(advanced_programming_2_server_side_exerciseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Contact> Get()
        {
            return _context.Contact.ToList();
        }
    }
}
