namespace TibberRobot.Domain.Features.RobotMovement.CommandValidators
{
    public interface ICommandValidator
    {
        bool IsValidCommand(decimal position, decimal limit);
    }
}