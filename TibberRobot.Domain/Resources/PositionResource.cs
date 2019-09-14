namespace TibberRobot.Domain.Resources
{
    public class PositionResource
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public bool isResourceValid()
        {
            if (X > 100000 || X < -100000 || Y > 100000 || Y < -100000)
            {
                return false;
            }

            return true;
        }
    }
}
