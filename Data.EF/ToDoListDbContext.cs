using Data.EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

            // Category:
            builder.Entity<Category>().HasKey(c => c.Id);

            builder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<User>()
                .Property(u => u.FirstName)
                .HasMaxLength(100);

            builder.Entity<User>()
                .Property(u => u.LastName)
                .HasMaxLength(100);

            builder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(256);

            // ATask:
            builder.Entity<ATask>().HasKey(t => t.Id);

            builder.Entity<ATask>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Entity<ATask>()
                .Property(t => t.Description)
                .HasMaxLength(500);

            builder.Entity<ATask>()
                .Property(t => t.CreatedDate)
                .IsRequired();

            // Entities Relationships:
            builder.Entity<ATask>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey("CategoryId"); // Assuming CategoryId is a foreign key property in ATask

            builder.Entity<ATask>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey("UserId"); // Assuming UserId is a foreign key property in ATask

            // Seed data:

            builder.Entity<Category>().HasData(new Category { Id = 1, Name = "Default Category" });

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
