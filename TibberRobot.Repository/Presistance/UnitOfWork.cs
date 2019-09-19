using System.Threading.Tasks;
using TibberRobot.Domain.Features.RobotMovement;

namespace TibberRobot.Repository.Presistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RobotDbContext context;
        public IMovementRepository MovementRepository { get; set; }

        public UnitOfWork(RobotDbContext context)
        {
            this.context = context;
            this.MovementRepository = new MovementRepository(context);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
