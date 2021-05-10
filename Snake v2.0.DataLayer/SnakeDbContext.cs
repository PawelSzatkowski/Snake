using Microsoft.EntityFrameworkCore;
using Snake_v2._0.DataLayer.Models;
using Snake_v2._0.Models;

namespace Snake_v2._0.DataLayer
{
    public class SnakeDbContext : DbContext
    {
        public DbSet<FeaturesState> FeaturesStates { get; set; }
        //public DbSet<Position> Positions { get; set; }
        public DbSet<HighScore> HighScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=Snake_Dev;Trusted_Connection=True");
        }
    }
}
