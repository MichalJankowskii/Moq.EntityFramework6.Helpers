namespace Moq.EntityFramework6.Helpers.Examples.Users
{
    using System.Data.Entity;
    using Entities;

    public class UsersContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}