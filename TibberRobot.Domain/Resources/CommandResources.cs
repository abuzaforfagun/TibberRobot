using System;

namespace TibberRobot.Domain.Resources
{
    public class CommandResources
    {
        readonly string[] directions = { "east", "west", "north", "south" };

        public string Direction { get; set; }
        public int Steps { get; set; }
        
        public bool isValid =>
            (Steps <= 0 || Steps > 100000 || (Array.Find(directions, d => d.Equals(Direction.ToLower())) == null))
                ? false
                : true;
    }
}
