using bill_payment.Domains;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace bill_payment.BillDbContext
{
    public class Bill_PaymentContext : DbContext
    {
        public Bill_PaymentContext(DbContextOptions<Bill_PaymentContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<UserPartners> UserPartners { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Banners> Banners { get; set; }
        public DbSet<FavouritePayments> FavouritePayments { get; set; }
        public DbSet<CreditCards> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(a => a.AdminId)
                .HasColumnType("uuid");
            modelBuilder.Entity<Banners>()
             .HasOne(a => a.setting).WithMany(x=> x.banners)
                         .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
