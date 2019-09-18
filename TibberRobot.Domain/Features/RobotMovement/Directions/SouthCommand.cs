using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class SouthCommand : CommandValidtor, ICommand
    {
        public decimal Limit { get; set; }

        public PositionResource GetNewPoint(decimal x, decimal y)
        {
            if (isValidCommand(y, Limit, false))
            {
                return new PositionResource(x, y - 1);
            }
            return null;
        }
    }
}
