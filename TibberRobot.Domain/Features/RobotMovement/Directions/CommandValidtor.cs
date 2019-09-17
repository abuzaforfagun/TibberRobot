namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class CommandValidtor : ICommandValidtor
    {
        public bool IsNextStepInvalidInNegativeGraph(decimal position, decimal limit) => position == limit || (limit > 0 && position <= 0);
        public bool IsNextStepInvalidInPositiveGraph(decimal position, decimal limit) => position == limit || (limit < 0 && position >= 0);
    }
}
