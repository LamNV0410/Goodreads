using Microsoft.EntityFrameworkCore;
using SMD.Goodreads.API.Models.Entities;

namespace SMD.Goodreads.API.Context
{
    public class GoodReadsDbcontext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<User> Users { get; set; }
        public GoodReadsDbcontext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserBook>().HasKey(sc => new { sc.UserId, sc.BookId});

            modelBuilder.Entity<UserBook>()
                .HasOne<Book>(sc => sc.Book)
                .WithMany(s => s.UserBooks)
                .HasForeignKey(sc => sc.BookId);

            modelBuilder.Entity<UserBook>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.UserBooks)
                .HasForeignKey(sc => sc.UserId);
        }
    }
}
