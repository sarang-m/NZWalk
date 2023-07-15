 using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;

        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            //check if user is authenticated
            bool isAuthenticated = await _userRepository.Authenticate(loginRequest.UserName, loginRequest.Password);
            if (!isAuthenticated)
            {
                return BadRequest("User name or Password is incorrect");
            }
            return View();
        }
    }
}
