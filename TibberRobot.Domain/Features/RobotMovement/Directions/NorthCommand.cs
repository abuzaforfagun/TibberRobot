using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class NorthCommand : CommandValidtor, ICommand
    {
        public decimal Limit { get; set; }

        public PositionResource GetNewPoint(decimal x, decimal y)
        {
            if (isValidCommand(y, Limit, true))
            {
                return new PositionResource(x, y + 1);
            }
            return null;
        }
    }
}
