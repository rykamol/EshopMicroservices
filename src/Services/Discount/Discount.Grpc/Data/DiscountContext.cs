using Discount.Grpc.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
	public class DiscountContext : DbContext
	{
		public DiscountContext(DbContextOptions<DiscountContext> options)
			: base(options)
		{ }

		public DbSet<Coupon> Coupons { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Coupon>().HasData
				(
					new Coupon
					{
						Id = 1,
						ProductName = "Wireless Headphones",
						Description = "Get $20 off on premium wireless headphones.",
						Amount = 20
					},
					new Coupon
					{
						Id = 2,
						ProductName = "Smartphone Case",
						Description = "Save $5 on any smartphone case.",
						Amount = 5
					}
				);
		}
	}
}
