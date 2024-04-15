using Microsoft.EntityFrameworkCore;

namespace AdsApi.Data
{
	public class AdsApiContext : DbContext
	{
		public AdsApiContext(DbContextOptions options) : base(options)
		{
		}

		protected AdsApiContext()
		{
		}

		public DbSet<Advert> Ads { get; set; }
	}
}
