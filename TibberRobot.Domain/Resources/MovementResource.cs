using System.Collections.Generic;
using System.Linq;

namespace TibberRobot.Domain.Resources
{
    public class MovementResource
    {
        public PositionResource Start { get; set; }
        public List<CommandsResources> Commands { get; set; }

        public bool isResourceValid()
        {
            if (Start != null && Start.isResourceValid() && Commands != null && Commands.Count > 0)
            {
                foreach (var commandsResourcese in Commands)
                {
                    if (!commandsResourcese.isValid())
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
