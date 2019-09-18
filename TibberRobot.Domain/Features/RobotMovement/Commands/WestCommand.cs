using TibberRobot.Domain.Features.RobotMovement.CommandValidators;
using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Commands
{
    public class WestCommand : NegativeGraphCommandValidator, ICommand
    {
        public decimal Limit { get; set; }

        public PositionResource GetNewPoint(decimal x, decimal y)
        {
            return IsValidCommand(x, Limit) ? new PositionResource(x - 1, y) : null;
        }
    }
}
