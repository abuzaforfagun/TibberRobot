using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class SouthCommand : ICommand
    {
        public PositionResource Start { get; set; }

        public bool IsNextStepInvalid(decimal x, decimal y) => y == Start.Y || (Start.Y > 0 && y <= 0);

        public PositionResource GetUniqueResource(decimal x, decimal y)
        {
            if (IsNextStepInvalid(x, y))
            {
                return null;
            }
            return new PositionResource(x, y - 1);
        }
    }
}
