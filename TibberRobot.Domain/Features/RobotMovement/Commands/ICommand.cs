using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Commands
{
    public interface ICommand
    {
        decimal Limit { get; set; }
        PositionResource GetNewPoint(decimal x, decimal y);
    }
}
