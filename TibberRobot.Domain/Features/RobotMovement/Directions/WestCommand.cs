using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class WestCommand : CommandValidtor, ICommand
    {
        public decimal Limit { get; set; }

        public PositionResource GetUniqueResource(decimal x, decimal y)
        {
            if (IsNextStepInvalidInNegativeGraph(x, Limit))
            {
                return null;
            }
            return new PositionResource(x - 1, y);
        }
    }
}
