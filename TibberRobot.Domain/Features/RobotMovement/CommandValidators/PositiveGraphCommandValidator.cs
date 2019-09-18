namespace TibberRobot.Domain.Features.RobotMovement.CommandValidators
{
    public class PositiveGraphCommandValidator : CommandValidator
    {
        public override bool IsValidCommand(decimal position, decimal limit) 
            => base.IsValidCommand(position, limit) && (position < 0 || limit > 0);
    }
}
