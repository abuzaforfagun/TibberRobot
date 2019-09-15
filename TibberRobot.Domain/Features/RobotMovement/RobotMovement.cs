using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TibberRobot.Domain.Resources;
using TibberRobot.Entities;
using TibberRobot.Repository.Presistance;

namespace TibberRobot.Domain.Features.RobotMovement
{
    public class RobotMovement : IRobotMovement
    {
        private readonly IUnitOfWork unitOfWork;

        public RobotMovement(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<int> FindUniqueCleanedPlacesAsync(MovementResource movement)
        {
            var entity = new Movement();
            entity.Timestamp = DateTime.Now;
            entity.Commands = movement.Commands.Count;
            entity.Result = GetUniqeCleanPoints(movement);
            unitOfWork.MovementRepository.Add(entity);
            var difference = DateTime.Now.Ticks - entity.Timestamp.Ticks;
            var differenceTimeSpan = new TimeSpan(difference);
            entity.Duration = Convert.ToDecimal(differenceTimeSpan.TotalSeconds);
            await unitOfWork.SaveChangesAsync();
            return entity.Result;
        }

        private int GetUniqeCleanPoints(MovementResource movement)
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
                }
                else if (direction == "west")
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
                }
                else if (direction == "north")
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
                }
                else if (direction == "south")
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
