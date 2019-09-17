namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public interface ICommandValidtor
    {
        bool IsNextStepInvalidInNegativeGraph(decimal position, decimal limit);
        bool IsNextStepInvalidInPositiveGraph(decimal position, decimal limit);
    }
}