using System.Collections.Generic;

namespace TibberRobot.Domain.Resources
{
    public class MovementResource
    {
        public PositionResource Start { get; set; }
        public List<CommandResources> Commands { get; set; }

        public bool isResourceValid()
        {
            if (Start != null && Start.isValid && Commands != null && Commands.Count > 0)
            {
                foreach (var command in Commands)
                {
                    if (!command.isValid)
                    {
                        return false;
                    }
                }

                return true;
            }
            return false;
        }
    }
}
