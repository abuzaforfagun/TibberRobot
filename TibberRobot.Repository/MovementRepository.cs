using TibberRobot.Domain.Features.RobotMovement;
using TibberRobot.Entities;
using TibberRobot.Repository.Presistance;

namespace TibberRobot.Repository
{

    public class MovementRepository : IMovementRepository
    {
        private readonly RobotDbContext context;

        public MovementRepository(RobotDbContext context)
        {
            this.context = context;
        }
        public void Add(Movement data)
        {
            context.Movement.Add(data);
        }
    }
}
