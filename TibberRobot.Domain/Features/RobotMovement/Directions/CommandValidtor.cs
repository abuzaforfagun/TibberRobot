namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class CommandValidtor : ICommandValidtor
    {
        public bool IsNextStepValidInNegativeGraph(decimal position, decimal limit) => position != limit && (limit < 0 || position > 0);
        public bool IsNextStepValidInPositiveGraph(decimal position, decimal limit) => position != limit && (limit > 0 || position < 0);
    }
}
