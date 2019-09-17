using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class EastCommand : ICommand
    {
        public PositionResource Start { get; set; }

        public bool IsNextStepInvalid(decimal x, decimal y) => x == Start.X || (Start.X < 0 && x >= 0);

        public PositionResource GetUniqueResource(decimal x, decimal y)
        {
            if (IsNextStepInvalid(x, y))
            {
                return null;
            }
            return new PositionResource(x + 1, y);
        }
    }
}
