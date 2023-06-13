using Microsoft.EntityFrameworkCore;
using ToDo_API.Models;

namespace ToDo_API
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        // Seed data
        public void SeedData()
        {
            Users.AddRange(new User
            {
                Username = "admin",
                PasswordHash = "123456",
                Role = Role.ADMIN
            },
            new User
            {
                Username = "user",
                PasswordHash = "123456",
                Role = Role.USER
            });

            SaveChanges();
        }

    }
}
