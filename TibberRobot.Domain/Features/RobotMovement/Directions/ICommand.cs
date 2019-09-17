using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public interface ICommand
    {
        PositionResource Start { get; set; }
        bool IsNextStepInvalid(decimal x, decimal y);
        PositionResource GetUniqueResource(decimal x, decimal y);
    }
}
