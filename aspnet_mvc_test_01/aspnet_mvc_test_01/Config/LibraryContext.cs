using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace aspnet_mvc_test_01.Config
{
    public class LibraryContext : DbContext
    {
        public DbSet<Models.Category> Category { get; set; }
        public DbSet<Models.Author> Author { get; set; }

        public DbSet<Models.Book> Book { get; set; }

        public DbSet<Models.Attachment> Attachment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("server=localhost;database=bookshop;user=root;password=''");
            optionsBuilder.UseMySQL("server=aspnetmvctest.mysql.database.azure.com;database=bookshop;user=neotdot_test01@aspnetmvctest;password=azure2539N!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Category>(entity =>
            {
                entity.HasKey(e => e.category_id);
                entity.Property(e => e.category_name).IsRequired();
                entity.Property(e => e.category_create_at).HasDefaultValueSql();
                entity.Property(e => e.category_update_at).HasDefaultValueSql();

            });

            modelBuilder.Entity<Models.Author>(entity =>
            {
                entity.HasKey(e => e.author_id);
                entity.Property(e => e.author_create_at).HasDefaultValueSql();
                entity.Property(e => e.author_update_at).HasDefaultValueSql();
            });

            modelBuilder.Entity<Models.Book>(entity =>
            {
                entity.HasKey(e => e.book_id);
                entity.Property(e => e.book_create_at).HasDefaultValueSql();
                entity.Property(e => e.book_update_at).HasDefaultValueSql();
            });

            modelBuilder.Entity<Models.Attachment>(entity =>
            {
                entity.HasKey(e => e.attachment_id);
                entity.Property(e => e.attachment_create_at).HasDefaultValueSql();
                entity.Property(e => e.attachment_update_at).HasDefaultValueSql();
            });
        }
    }
}
