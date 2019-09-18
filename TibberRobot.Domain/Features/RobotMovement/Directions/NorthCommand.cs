using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class NorthCommand : CommandValidtor, ICommand
    {
        public decimal Limit { get; set; }

        public PositionResource GetNewPoint(decimal x, decimal y)
        {
            if (IsNextStepValidInPositiveGraph(y, Limit))
            {
                return new PositionResource(x, y + 1);
            }
            return null;
        }
    }
}
