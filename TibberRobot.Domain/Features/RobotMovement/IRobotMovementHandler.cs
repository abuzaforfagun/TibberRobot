using System.Threading.Tasks;
using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement
{
    public interface IRobotMovementHandler
    {
        Task<int> FindUniqueCleanedPlacesAsync(MovementResource movement);
    }
}
