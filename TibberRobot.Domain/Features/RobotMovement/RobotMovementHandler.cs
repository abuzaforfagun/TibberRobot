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
            var uniquePlaces = new List<PositionResource>();
            int _x = 0;
            int _y = 0;
            var directions = mapper.Map<MovementResource, IEnumerable<string>>(movement);
            var lastIgnoredDirection = "";

            //todo: improve the code structure
            foreach (var _direction in directions)
            {
                if (_direction == "east")
                {
                    if (CanMoveOnPositivePosition(movement.Start.X, _x))
                    {
                        continue;
                    }
                    _x++;
                }
                else if (_direction == "west")
                {
                    if (CanMoveOnNegativePosition(movement.Start.X, _x))
                    {
                        continue;
                    }
                    _x--;
                }
                else if (_direction == "north")
                {
                    if (CanMoveOnPositivePosition(movement.Start.Y, _y))
                    {
                        continue;
                    }
                    _y++;
                }
                else if (_direction == "south")
                {
                    if (CanMoveOnNegativePosition(movement.Start.Y, _y))
                    {
                        continue;
                    }
                   _y--;
                }
                AddPath(uniquePlaces, _x, _y);

            }
            return uniquePlaces.Count;
        }

        private static bool CanMoveOnPositivePosition(decimal limit, int position)
        {
            return position == limit || (limit < 0 && position >= 0);
        }

        private static bool CanMoveOnNegativePosition(decimal limit, int position)
        {
            return position == limit || (limit > 0 && position <= 0);
        }

        private static void AddPath(List<PositionResource> uniquePlaces, int _x, int _y)
        {
            if (uniquePlaces.Count(r => r.Y == _y && r.X == _x) == 0)
            {
                uniquePlaces.Add(new PositionResource { X = _x, Y = _y });
            }
        }
    }
}
