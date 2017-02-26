﻿namespace Core
{
    using System.Data.Entity;

    public class UsersContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}