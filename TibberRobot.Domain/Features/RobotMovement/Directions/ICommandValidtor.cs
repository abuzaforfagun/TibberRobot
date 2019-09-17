namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public interface ICommandValidtor
    {
        bool IsNextStepValidInNegativeGraph(decimal position, decimal limit);
        bool IsNextStepValidInPositiveGraph(decimal position, decimal limit);
    }
}