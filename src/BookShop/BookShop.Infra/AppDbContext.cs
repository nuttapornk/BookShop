using BookShop.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<BookImage> BookImages { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public virtual DbSet<GoodsReceiptItem> GoodsReceiptItems { get; set; }
        public virtual DbSet<MovementType> MovementTypes { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockMovement> StockMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Entity<Book>().Config(
                    //a => a.HasAlternateKey(a => a.Isbn),
                    a => a.Property(a => a.TimeInsert).HasDefaultValueSql("getdate()"),
                    a => a.Property(a => a.TimeUpdate).HasDefaultValueSql("getdate()")
                );

                modelBuilder.Entity<BookCategory>().Config(
                   a => a.HasKey(a => new { a.BookId, a.CategoryId })
                   );

                modelBuilder.Entity<BookImage>().Config(
                    a => a.Property(a => a.TimeInsert).HasDefaultValueSql("getdate()"),
                    a => a.Property(a => a.TimeUpdate).HasDefaultValueSql("getdate()")
                );

                modelBuilder.Entity<Category>().Config(
                    a => a.Property(a => a.TimeInsert).HasDefaultValueSql("getdate()"),
                    a => a.Property(a => a.TimeUpdate).HasDefaultValueSql("getdate()")
                    );

                modelBuilder.Entity<GoodsReceipt>().Config(
                    a => a.Property(a => a.TimeInsert).HasDefaultValueSql("getdate()")
                    );

                modelBuilder.Entity<GoodsReceiptItem>().Config(
                    a => a.HasKey(a => new { a.GoodsReceiptId, a.Num })
                );

                modelBuilder.Entity<MovementType>().Config(
                   a => a.Property(a => a.TimeInsert).HasDefaultValueSql("getdate()"),
                   a => a.Property(a => a.TimeUpdate).HasDefaultValueSql("getdate()")
                   );

                modelBuilder.Entity<Publisher>().Config(
                    a => a.Property(a => a.TimeInsert).HasDefaultValueSql("getdate()"),
                    a => a.Property(a => a.TimeUpdate).HasDefaultValueSql("getdate()")
                    );

                modelBuilder.Entity<Stock>().Config(
                    a => a.HasKey(a => a.BookId)
                    );

                modelBuilder.Entity<StockMovement>().Config(
                    a => a.Property(a => a.TimeInsert).HasDefaultValueSql("getdate()")
                    );

            }
        }
    }
}
