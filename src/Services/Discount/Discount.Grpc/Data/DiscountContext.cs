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
	}
}
