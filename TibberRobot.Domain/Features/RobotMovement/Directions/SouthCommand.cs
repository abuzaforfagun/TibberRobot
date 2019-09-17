using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class SouthCommand : CommandValidtor, ICommand
    {
        public decimal Limit { get; set; }

        public PositionResource GetNewPosition(decimal x, decimal y)
        {
            if (IsNextStepValidInNegativeGraph(y, Limit))
            {
                return new PositionResource(x, y - 1);
            }
            return null;
        }
    }
}
