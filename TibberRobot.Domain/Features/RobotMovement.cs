using System.Collections.Generic;
using System.Linq;
using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features
{
    public class RobotMovement : IRobotMovement
    {
        public int FindUniqueCleanedPlaces(MovementResource movement)
        {
            List<PositionResource> res = new List<PositionResource>();
            PositionResource lastPosition = new PositionResource();
            lastPosition = movement.Start;
            res.Add(movement.Start);
            //todo: improve the code structure
            foreach (var command in movement.Commands)
            {
                var direction = command.Direction;
                if (direction == "east")
                {
                    for (int i = 0; i < command.Steps; i++)
                    {
                        var pos = new PositionResource();
                        pos.X = lastPosition.X + 1;
                        pos.Y = lastPosition.Y;
                        if (res.Count(r => r.Y == pos.Y && r.X == pos.X) == 0)
                        {
                            res.Add(pos);
                        }
                        lastPosition = pos;
                    }
                } else if (direction == "west")
                {
                    for (int i = 0; i < command.Steps; i++)
                    {
                        var pos = new PositionResource();
                        pos.X = lastPosition.X - 1;
                        pos.Y = lastPosition.Y;
                        if (res.Count(r => r.Y == pos.Y && r.X == pos.X) == 0)
                        {
                            res.Add(pos);
                        }
                        lastPosition = pos;
                    }
                } else if (direction == "north")
                {
                    for (int i = 0; i < command.Steps; i++)
                    {
                        var pos = new PositionResource();
                        pos.X = lastPosition.X;
                        pos.Y = lastPosition.Y + 1;
                        if (res.Count(r => r.Y == pos.Y && r.X == pos.X) == 0)
                        {
                            res.Add(pos);
                        }
                        lastPosition = pos;
                    }
                } else if (direction == "south")
                {
                    for (int i = 0; i < command.Steps; i++)
                    {
                        var pos = new PositionResource();
                        pos.X = lastPosition.X;
                        pos.Y = lastPosition.Y - 1;
                        if (res.Count(r => r.Y == pos.Y && r.X == pos.X) == 0)
                        {
                            res.Add(pos);
                        }
                        lastPosition = pos;
                    }
                }
            }

            return res.Count;
        }
    }
}
