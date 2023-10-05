using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<Press> Presses { get; set; }
        public DbSet<Book> Books { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Press>().HasData(
                    new Press()
                    {
                        Id = 1,
                        Name = "Addison-Wesley",
                        Category = Category.Book
                    }
                );

            builder.Entity<Book>().HasData(
                    new Book()
                    {
                        Id = 1,
                        ISBN = "987-0-321-87758-1",
                        Title = "Essential C#5.0",
                        Author = "Mark Michaelis",
                        Price = 59.99m,
                        PressId = 1,
                    }
                );

            builder.Entity<Book>().OwnsOne(b => b.Location).HasData(
                    new
                    {
                        BookId = 1,
                        City = "HCM City",
                        Street = "D2, Thu Duc District"
                    }
                );
        }
    }
}