using System;

namespace TibberRobot.Entities
{
    public class Movement
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int Commands { get; set; }
        public int Result { get; set; }
        public decimal Duration { get; set; }
    }
}
