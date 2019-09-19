namespace TibberRobot.Domain.Features.RobotMovement.CommandValidators
{
    public class CommandValidator : ICommandValidator
    {
        public virtual bool IsValidCommand(decimal position, decimal limit) => position != limit;
    }
}
