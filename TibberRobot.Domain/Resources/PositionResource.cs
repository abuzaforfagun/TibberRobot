using System;

namespace TibberRobot.Domain.Resources
{
    public class PositionResource
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public bool isValid => (X > 100000 || X < -100000 || Y > 100000 || Y < -100000) ? false : true;

        public PositionResource(decimal X, decimal Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public PositionResource()
        {
            
        }

        public override int GetHashCode()
        {
            return (X + Y).GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            PositionResource otherObject = obj as PositionResource;
            return otherObject != null && otherObject.X == this.X && otherObject.Y == this.Y;
        }
    }
}
