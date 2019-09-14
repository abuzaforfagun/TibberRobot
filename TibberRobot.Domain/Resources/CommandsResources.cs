using System;
using System.Linq;

namespace TibberRobot.Domain.Resources
{
    public class CommandsResources
    {
        public string Direction { get; set; }
        public int Steps { get; set; }

        string[] directions = Enum.GetNames(typeof(Enums.Direction));

        public bool isValid()
        {
            if (Steps <= 0 || Steps > 100000 || (Array.Find(directions, d => d.ToLower().Equals(Direction)) == null))
            {
                return false;
            }

            return true;
        }
    }
}
