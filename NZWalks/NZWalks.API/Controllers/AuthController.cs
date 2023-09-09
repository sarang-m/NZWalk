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
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this._userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            //check if user is authenticated
            var user = await _userRepository.Authenticate(loginRequest.UserName, loginRequest.Password);
            if (user != null)
            {
                 //Generate JWT token
                var token = await tokenHandler.CreateToken(user);
                return Ok(token);
            }
            return BadRequest("Username or password is incorrect");
            
        }
    }
}
