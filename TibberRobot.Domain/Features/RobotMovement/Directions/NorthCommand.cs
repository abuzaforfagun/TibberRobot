using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class NorthCommand : CommandValidtor, ICommand
    {
        public decimal Limit { get; set; }

        public PositionResource GetUniqueResource(decimal x, decimal y)
        {
            if (IsNextStepInvalidInPositiveGraph(y, Limit))
            {
                return null;
            }
            return new PositionResource(x, y + 1);
        }
    }
}
