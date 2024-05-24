using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Identity.Entities;

namespace MVC_Identity.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Lion> Lions { get; set; }
        public DbSet<Zoo> Zoos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Animal());
            modelBuilder.ApplyConfiguration(new Lion());
            modelBuilder.ApplyConfiguration(new Zoo());


            modelBuilder.Entity<Lion>().HasData(
                new Lion

                {
                    Id = Guid.NewGuid(),
                    Name = "Ahura Mazda",
                    Specie = "Leão Africano",
                    Age = 5,
                    BirthPlace = "África"
                });


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string? connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}