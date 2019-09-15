using TibberRobot.Entities;

namespace TibberRobot.Domain.Features.RobotMovement
{
    public interface IMovementRepository
    {
        void Add(Movement data);
    }
}
