using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features
{
    public interface IRobotMovement
    {
        int FindUniqueCleanedPlaces(MovementResource movement);
    }
}
