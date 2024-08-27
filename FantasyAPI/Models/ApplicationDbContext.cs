using Microsoft.EntityFrameworkCore;

namespace FantasyAPI.Models
{
	public class ApplicationDbContext:DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure Team-Player relationship
			modelBuilder.Entity<Player>()
				.HasOne(p => p.Team)
				.WithMany(t => t.Players)
				.HasForeignKey(p => p.TeamId)
				.OnDelete(DeleteBehavior.Cascade); // Adjust behavior as needed
		}
	}
}
