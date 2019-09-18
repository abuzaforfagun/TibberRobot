using TibberRobot.Domain.Features.RobotMovement.CommandValidators;
using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Commands
{
    public class SouthCommand : NegativeGraphCommandValidator, ICommand
    {
        public decimal Limit { get; set; }

        public PositionResource GetNewPoint(decimal x, decimal y)
        {
            return IsValidCommand(y, Limit) ? new PositionResource(x, y - 1) : null;
        }
    }
}
