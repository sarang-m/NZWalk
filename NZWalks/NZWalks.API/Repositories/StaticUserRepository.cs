using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                Id = Guid.NewGuid(),
                FirstName= "Read only",
                LastName = "User",
                Email = "readonlyuser@user.com",
                Username = "readonlyuser@user.com",
                Password = "password",
                Roles = new List<string>{"reader"}
            },
            new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Read Write",
                LastName = "User",
                Email = "readwriteuser@user.com",
                Username = "readwriteuser@user.com",
                Password = "password",
                Roles = new List<string>{"reader", "writer"}
            }
        };
        public async Task<User> Authenticate(string username, string password)
        {
            var user = Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && x.Password == password);
            return user;
        }
    }
}
