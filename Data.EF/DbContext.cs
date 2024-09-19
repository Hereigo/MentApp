using Data.EF.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = Data.EF.Models.Task;

namespace Data.EF
{
    public class DbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Task> Tasks { get; set; }
        
        public DbSet<User> Users { get; set; }
    }
}
