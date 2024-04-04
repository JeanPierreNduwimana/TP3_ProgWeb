
using Microsoft.EntityFrameworkCore;
using FlappyBird_WebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FlappyBird_WebAPI.Data
{
    public class FlappyBird_WebAPIContext : IdentityDbContext<User>
    {
        public FlappyBird_WebAPIContext (DbContextOptions<FlappyBird_WebAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            // Seed pour les users
            User u1 = new User
            {
                Id = "11111111-1111-1111-1111-111111111111",
                UserName = "jeanpierre",
                NormalizedUserName = "JEANPIERRE",
                Email = "jean@exemple.com",
                NormalizedEmail = "JEAN@EXEMPLE.COM",
            };
            u1.PasswordHash = passwordHasher.HashPassword(u1, "asdfgh");

            User u2 = new User
            {
                Id = "11111111-1111-1111-1111-111111111112",
                UserName = "mario",
                NormalizedUserName = "MARIO",
                Email = "mario@exemple.com",
                NormalizedEmail = "MARIO@EXEMPLE.COM",
            };
            u2.PasswordHash = passwordHasher.HashPassword(u1, "asdfgh");

            builder.Entity<User>().HasData(u1);
            builder.Entity<User>().HasData(u2);


            //Seed pour les scores

            builder.Entity<Score>().HasData(new
            {
                id = 1,
                timeInSeconds = "300.999",
                isPublic = true,
                scoreValue = 298,
                UserId = u1.Id,
                date = "2024-04-04 15:45:03",
                pseudo = "jeanpierre",
            });

            builder.Entity<Score>().HasData(new
            {
                id = 2,
                timeInSeconds = "5.125",
                isPublic = false,
                scoreValue = 3,
                UserId = u1.Id,
                date = "2024-04-02 12:39:03",
                pseudo = "jeanpierre",
            });

            builder.Entity<Score>().HasData(new
            {
                id = 3,
                timeInSeconds = "255.125",
                isPublic = true,
                scoreValue = 252,
                UserId = u2.Id,
                date = "2024-04-04 12:39:03",
                pseudo = "mario",
            });

            builder.Entity<Score>().HasData(new
            {
                id = 4,
                timeInSeconds = "55.125",
                isPublic = false,
                scoreValue = 52,
                UserId = u2.Id,
                date = "2024-04-02 12:39:03",
                pseudo = "mario",
            });

        }

        public DbSet<Score> Score { get; set; } = default!;
    }
}
