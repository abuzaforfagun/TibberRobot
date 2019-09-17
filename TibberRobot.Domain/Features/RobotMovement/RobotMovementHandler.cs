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
            int _x = 0;
            int _y = 0;
            //todo: improve the code structure
            foreach (var command in movement.Commands)
            {
                if (command.Direction == "east")
                {
                    for (int i = 0; i < command.Steps; i++)
                    {
                        if (_x == movement.Start.X || (movement.Start.X < 0 && _x >= 0))
                        {
                            break;
                        }
                        _x++;
                        AddPath(res, _x, _y);
                    }
                }
                else if (command.Direction == "west")
                {
                    for (int i = 0; i < command.Steps; i++)
                    {
                        if (_x == movement.Start.X || (movement.Start.X > 0 && _x <= 0))
                        {
                            break;
                        }
                        _x--;
                        AddPath(res, _x, _y);
                    }
                }
                else if (command.Direction == "north")
                {
                    for (int i = 0; i < command.Steps; i++)
                    {
                        if (_y == movement.Start.Y || (movement.Start.Y < 0 && _y >= 0))
                        {
                            break;
                        }
                        _y++;
                        AddPath(res, _x, _y);
                    }
                }
                else if (command.Direction == "south")
                {
                    for (int i = 0; i < command.Steps; i++)
                    {
                        if (_y == movement.Start.Y || (movement.Start.Y > 0 && _y <= 0))
                        {
                            break;
                        }
                        _y--;
                        AddPath(res, _x, _y);
                    }
                }
            }
            return res.Count;
        }

        private static void AddPath(List<PositionResource> res, int _x, int _y)
        {
            if (res.Count(r => r.Y == _y && r.X == _x) == 0)
            {
                res.Add(new PositionResource { X = _x, Y = _y });
            }
        }
    }
}
