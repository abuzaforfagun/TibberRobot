using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement
{
    public interface IRobotMovementHelper
    {
        int GetCleanPoints(MovementResource movement);
    }
}