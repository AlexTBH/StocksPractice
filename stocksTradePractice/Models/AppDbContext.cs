using Microsoft.EntityFrameworkCore;

namespace stocksTradePractice.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
		{
		}

		public DbSet<User> Users { get; set; }
	}

	
}
