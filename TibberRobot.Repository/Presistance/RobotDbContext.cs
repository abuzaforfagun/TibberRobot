using Microsoft.EntityFrameworkCore;
using TibberRobot.Entities;

namespace TibberRobot.Repository.Presistance
{
    public class RobotDbContext : DbContext
    {
        public DbSet<Movement> Movement { get; set; }

        public RobotDbContext(DbContextOptions<RobotDbContext> options)
            : base(options)
        {
        }
    }
}
