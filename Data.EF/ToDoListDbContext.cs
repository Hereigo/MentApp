using Data.EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ATask = Data.EF.Models.ATask;

namespace Data.EF
{
    public class ToDoListDbContext : IdentityDbContext<IdentityUser>
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ATask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string ROLE_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf7";

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            var newUser = new User
            {
                Id = ADMIN_ID,
                Email = "admin@aa.aa",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "SuperAdmin",
                UserName = "admin@aa.aa",
                NormalizedUserName = "ADMIN@AA.AA"
            };

            PasswordHasher<User> ph = new PasswordHasher<User>();
            newUser.PasswordHash = ph.HashPassword(newUser, "mypassword_?");

            builder.Entity<User>().HasData(newUser);

            builder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.ToTable("AspNetUserRoles");
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
