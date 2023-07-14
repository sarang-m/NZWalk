using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        
        public Task<bool> Authenticate(string username, string password);
    }
}
