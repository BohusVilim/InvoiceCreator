using InvoiceCreator.Models.MainModels;
using Microsoft.EntityFrameworkCore;
using InvoiceCreator.Models.EmailModels;

namespace InvoiceCreator.InvoiceCreatorDbContext
{
    public class InvoiceCreatorDbContext : DbContext
    {
        public InvoiceCreatorDbContext(DbContextOptions<InvoiceCreatorDbContext> options)
            : base(options)
        {
        }
        public DbSet<Invoice> Invoices { get; set; } = default!;
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Costumer> Costumers { get; set; }
        public DbSet<PaymentData> PaymentDatas { get; set; }
        public DbSet<Service> Services { get; set; }

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

        public DbSet<InvoiceCreator.Models.EmailModels.EmailModel> EmailModel { get; set; } = default!;
    }
}
