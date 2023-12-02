using Microsoft.EntityFrameworkCore;

namespace AspWebTest2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> CUSTOMER { get; set; }
        public DbSet<Product> PRODUCT { get; set; }
        public DbSet<ORDERLIST> ORDERLIST { get; set; }
        public DbSet<OrderDetail> ORDERDETAILS { get; set; }
        public DbSet<Address> CustomerAddress { get; set; }
        public DbSet<AddressInfo> AddressInformation { get; set; }
        public DbSet<Hub> DistributionHUB { get; set; }
        public DbSet<Driver> DriverInformation { get; set; }
        public DbSet<Inquiry> INQUIRY { get; set; }
        public DbSet<FirstFreightShipping> FirstFreightShipping { get; set; }
        public DbSet<SecondFreightShipping> SecondFreightShipping { get; set; }
        public DbSet<Review> REVIEW { get; set; }
        public DbSet<Membership> MEMBERSHIP { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 주 키 설정
            modelBuilder.Entity<ORDERLIST>().HasKey(o => o.OrderID);
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderID, od.ProductID });
            modelBuilder.Entity<Address>().HasKey(a => a.DetailedAddress);
            modelBuilder.Entity<AddressInfo>().HasKey(ai => ai.LocalAddress);
            modelBuilder.Entity<Hub>().HasKey(h => h.DistributionName);
            modelBuilder.Entity<Driver>().HasKey(d => d.CargoDriverID);
            modelBuilder.Entity<Inquiry>().HasKey(i => i.InquiryNumber);
            modelBuilder.Entity<FirstFreightShipping>().HasKey(ffs => ffs.TransportNumber);
            modelBuilder.Entity<SecondFreightShipping>().HasKey(sfs => sfs.TransportNumber);
            modelBuilder.Entity<Review>().HasKey(r => r.ReviewNumber);
            modelBuilder.Entity<Membership>().HasKey(m => m.CustomerID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
