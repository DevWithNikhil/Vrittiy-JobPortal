using Microsoft.AspNetCore.Mvc;
using Vrittiy.API.DTOs;
using Vrittiy.Core.Entities;
using Vrittiy.Infrastructure.Data;

namespace Vrittiy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        
        
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context) 
        {  
            _context = context; 
        }



        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = dto.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message = "User Registered Successfully"
            });
        }
    }
}
