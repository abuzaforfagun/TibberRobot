using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public interface ICommand
    {
        decimal Limit { get; set; }
        PositionResource GetNewPosition(decimal x, decimal y);
    }
}
