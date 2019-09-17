using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class SouthCommand : CommandValidtor, ICommand
    {
        public decimal Limit { get; set; }

        public PositionResource GetUniqueResource(decimal x, decimal y)
        {
            if (IsNextStepInvalidInNegativeGraph(y, Limit))
            {
                return null;
            }
            return new PositionResource(x, y - 1);
        }
    }
}
