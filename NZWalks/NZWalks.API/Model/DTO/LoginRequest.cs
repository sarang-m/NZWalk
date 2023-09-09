using NZWalks.API.Repositories;

namespace NZWalks.API.Model.DTO
{
    public class LoginRequest
    {
        
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
