using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        
        public Task<User> Authenticate(string username, string password);
    }
}
