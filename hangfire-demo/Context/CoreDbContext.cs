using hangfire_demo.Model;
using Microsoft.EntityFrameworkCore;

namespace hangfire_demo.Context
{
	public class CoreDbContext: DbContext
	{
		public CoreDbContext(DbContextOptions<CoreDbContext> options)
	   : base(options)
		{
		}

		public DbSet<PriceTracker> DailyPrices { get; set; }

		public DbSet<Currency> Currencies { get; set; }
	}
}
