using InvoiceCreator.Models.MainModels;
using Microsoft.EntityFrameworkCore;

namespace InvoiceCreator.InvoiceCreatorDbContext
{
    public class InvoiceCreatorInMemoryDbContext : DbContext
    {
        public InvoiceCreatorInMemoryDbContext(DbContextOptions<InvoiceCreatorInMemoryDbContext> options)
            : base(options)
        {
        }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Costumer> Costumers { get; set; }
        public DbSet<PaymentData> PaymentDatas { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Searching> Searchings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InvoiceCreatorInMemory");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Costumer>()
                 .HasOne(c => c.Invoice)
                 .WithOne(i => i.Costumer)
                 .HasForeignKey<Invoice>(i => i.CostumerId);

            modelBuilder.Entity<Supplier>()
                .HasOne(c => c.Invoice)
                .WithOne(i => i.Supplier)
                .HasForeignKey<Invoice>(i => i.SupplierId);

            modelBuilder.Entity<PaymentData>()
                .HasOne(c => c.Supplier)
                .WithOne(i => i.PaymentData)
                .HasForeignKey<Supplier>(i => i.PaymentDataId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

