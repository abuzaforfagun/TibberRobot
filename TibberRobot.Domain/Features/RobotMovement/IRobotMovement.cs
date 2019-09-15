using System.Threading.Tasks;
using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement
{
    public interface IRobotMovement
    {
        Task<int> FindUniqueCleanedPlacesAsync(MovementResource movement);
    }
}
