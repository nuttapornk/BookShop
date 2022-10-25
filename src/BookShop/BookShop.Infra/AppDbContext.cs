using BookShop.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public DbSet<GoodsReceiptItem> GoodsReceiptItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Entity<Book>().Config(
                    a => a.HasAlternateKey(a => a.Isbn),
                    a => a.Property(a => a.TimeInsert).HasDefaultValueSql("getdate()"),
                    a => a.Property(a => a.TimeUpdate).HasDefaultValueSql("getdate()")
                );

                modelBuilder.Entity<GoodsReceiptItem>().Config(
                    a => a.HasKey(a => new { a.GoodsReceiptId, a.Num })
                );
            }
        }
    }
}
