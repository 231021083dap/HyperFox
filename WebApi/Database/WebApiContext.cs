using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Database
{
    public class WebApiContext : DbContext
    {
        public WebApiContext()    {}
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options) {}

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "TestUserName",
                    Email = "TestMail",
                    Password = "TestPassword",
                    Admin = "Admin"

                });
        }

    }
}
