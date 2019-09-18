namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public interface ICommandValidtor
    {
        bool isValidCommand(decimal position, decimal limit, bool isPositiveGraph);
    }
}