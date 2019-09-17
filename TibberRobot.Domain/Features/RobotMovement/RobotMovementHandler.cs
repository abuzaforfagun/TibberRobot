using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TibberRobot.Domain.Features.RobotMovement.Directions;
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
            decimal _x = 0;
            decimal _y = 0;
            var directions = mapper.Map<MovementResource, IEnumerable<ICommand>>(movement);

            //todo: improve the code structure
            foreach (var _direction in directions)
            {
                var lastPosition = _direction.GetUniqueResource(_x, _y);
                if (lastPosition == null)
                {
                    continue;
                }
                _x = lastPosition.X;
                _y = lastPosition.Y;

                AddPath(uniquePlaces, _x, _y);

            }
            return uniquePlaces.Count;
        }

        private static void AddPath(List<PositionResource> uniquePlaces, decimal _x, decimal _y)
        {
            if (uniquePlaces.Count(r => r.Y == _y && r.X == _x) == 0)
            {
                uniquePlaces.Add(new PositionResource { X = _x, Y = _y });
            }
        }
    }
}
