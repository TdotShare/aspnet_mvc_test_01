using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace aspnet_mvc_test_01.Config
{
    public class LibraryContext : DbContext
    {
        public DbSet<Models.Category> Category { get; set; }
        public DbSet<Models.Author> Author { get; set; }

        public DbSet<Models.Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=bookshop;user=root;password=''");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Category>(entity =>
            {
                entity.HasKey(e => e.category_id);
            });

            modelBuilder.Entity<Models.Author>(entity =>
            {
                entity.HasKey(e => e.author_id);
            });

            modelBuilder.Entity<Models.Book>(entity =>
            {
                entity.HasKey(e => e.book_id);
            });
        }
    }
}
