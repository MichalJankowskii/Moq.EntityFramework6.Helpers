namespace Core
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersService
    {
        private readonly UsersContext usersContext;

        public UsersService(UsersContext usersContext)
        {
            this.usersContext = usersContext;
        }

        public User AddUser(string login, string name, string surname)
        {
            var newUser = this.usersContext.Users.Add(
                new User
                {
                    Login = login,
                    Name = name,
                    Surname = surname,
                    AccountLocked = false
                });

            this.usersContext.SaveChanges();
            return newUser;
        }

        public IList<User> GetLockedUsers()
        {
            return this.usersContext.Users.Where(x => x.AccountLocked).ToList();
        }

        public async Task<IList<User>> GetLockedUsersAsync()
        {
            return await this.usersContext.Users.Where(x => x.AccountLocked).ToListAsync();
        }
    }
}