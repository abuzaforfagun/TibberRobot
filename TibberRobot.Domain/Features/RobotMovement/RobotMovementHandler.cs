using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TibberRobot.Domain.Resources;
using TibberRobot.Entities;
using TibberRobot.Repository.Presistance;

namespace TibberRobot.Domain.Features.RobotMovement
{
    public class RobotMovementHandler : IRobotMovementHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RobotMovementHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<int> FindUniqueCleanedPlacesAsync(MovementResource movement)
        {
            var entity = mapper.Map<MovementResource, Movement>(movement);
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
            var res = new List<PositionResource>();
            var lastPosition = new PositionResource();
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
