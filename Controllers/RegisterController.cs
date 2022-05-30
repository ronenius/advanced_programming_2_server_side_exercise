using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_programming_2_server_side_exercise.Data;
using advanced_programming_2_server_side_exercise.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using advanced_programming_2_server_side_exercise.Services;

namespace advanced_programming_2_server_side_exercise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _service;

        public RegisterController(advanced_programming_2_server_side_exerciseContext context, IConfiguration config)
        {
            _service = new UserService(context);
            _configuration = config;
        }

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Username,Password,Name")] User user)
        {
            if (ModelState.IsValid)
            {
                List<User> users = await _service.GetAll();
                foreach (User currUser in users)
                {
                    if (currUser.Username == user.Username)
                    {
                        return BadRequest();
                    }
                }
                await _service.Create(user.Username, user.Password, user.Name);
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["JWTParams:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                    new Claim("UserId",user.Username)
                };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
                var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["JWTParams:Issuer"],
                    _configuration["JWTParams:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(20),
                    signingCredentials: mac);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return BadRequest();
        }
    }
}
