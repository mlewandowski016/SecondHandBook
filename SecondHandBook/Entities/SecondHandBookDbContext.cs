using Microsoft.EntityFrameworkCore;
using SecondHandBook.Models;

namespace SecondHandBook.Entities
{
    public class SecondHandBookDbContext : DbContext
    {
        public SecondHandBookDbContext(DbContextOptions<SecondHandBookDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<MyBook> MyBooks { get; set; }
        public DbSet<BookOffer> BookOffers { get; set; }
        public DbSet<BookOfferImage> BookOfferImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    } 
}
