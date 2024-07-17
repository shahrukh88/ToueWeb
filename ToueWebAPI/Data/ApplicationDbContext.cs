using Microsoft.EntityFrameworkCore;
using ToueWebAPI.Models;

namespace ToueWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Country>Countries { get; set; }
        public DbSet<City> Cities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country()
                {
                    Id = 1,
                    Name = "Pakistan",
                    Description = " Pakistan in Asia ",
                    Contenent = "Asia",
                    ImageUrl = "https://media.istockphoto.com/id/182820726/photo/pakistan-flag.jpg?s=170667a&w=0&k=20&c=agCnt4sci9t5JN04AzZptdjGhMHE3huGn4RV35no5UM=",
                    CreatedDate= DateTime.Now
                },
                new Country()
                {
                    Id = 2,
                    Name = "Eng",
                    Description = " Eng in Europe ",
                    Contenent = "Europe",
                    ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fstock.adobe.com%2Fsearch%2Fimages%3Fk%3Dengland%2Bflag&psig=AOvVaw3BaBgON3lhqW85O4yRVmZl&ust=1720108181970000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCOjv6L2ci4cDFQAAAAAdAAAAABAE",
                    CreatedDate = DateTime.Now
                }
                );
        }
    }
}
