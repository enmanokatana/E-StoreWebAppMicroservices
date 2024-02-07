using Microsoft.EntityFrameworkCore;
using Mongo.Services.CouponAPI.Models;

namespace Mongo.Services.CouponAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ): base(options)
        {
            
        }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "Test",
                DiscountAmount = 1,
                MinAmount = 1
            });
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "Test2",
                DiscountAmount = 2,
                MinAmount = 12
            });
        }
    }
}
