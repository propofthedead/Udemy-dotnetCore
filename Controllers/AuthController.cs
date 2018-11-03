using System.Threading.Tasks;
using DatingApp.Api.Data;
using DatingApp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo=repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password){

            username = username.ToLower();

            if(await _repo.UserExists(username))
                return BadRequest("Username already exits");

            var userToCreate = new User
            {
                Username=username
            };

            var createdUser = await _repo.Register(userToCreate,password);

            return StatusCode(201); 
        }

    }
}