namespace TibberRobot.Domain.Features.RobotMovement.Directions
{
    public class CommandValidtor : ICommandValidtor
    {
        private bool IsNextStepValidInNegativeGraph(decimal position, decimal limit) => position != limit && (limit < 0 || position > 0);
        private bool IsNextStepValidInPositiveGraph(decimal position, decimal limit) => position != limit && (limit > 0 || position < 0);

        public bool isValidCommand(decimal position, decimal limit, bool isPositiveGraph)
        {
            if (position == limit)
            {
                return false;
            }

            if (isPositiveGraph)
            {
                return IsNextStepValidInPositiveGraph(position, limit);
            }

            return IsNextStepValidInNegativeGraph(position, limit);
        }
    }
}
