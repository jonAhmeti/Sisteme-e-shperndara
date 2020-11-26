using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;
using WebApiTest.Security;

namespace WebApiTest.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("")]
    [Route("[controller]")]
    [Route("Login/[action]")]
    public class AuthenticateController : Controller
    {
        private TokenService tokenHandler;
        private WebAPIContext _context;
        public AuthenticateController(TokenService tokenService, WebAPIContext context)
        {
            tokenHandler = tokenService;
            _context = context;
        }

        [Route("")]
        [Route("login")]
        [HttpGet]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            var userDb = _context.Users.FirstOrDefault(element =>
                element.Username == user.Username && element.Password == user.Password);

            if (!ModelState.IsValid || userDb == null)
            {
                ModelState.AddModelError("Error", "Model from body is invalid.");
                return BadRequest(ModelState);
            }

            string token = tokenHandler.GenerateToken(userDb);
            userDb.Token = token;

            await _context.SaveChangesAsync();
            return Ok(token);
        }
    }
}