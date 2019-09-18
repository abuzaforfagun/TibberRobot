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
        public async Task<int> HandleAsync(MovementResource movement)
        {
            var entity = mapper.Map<MovementResource, Movement>(movement);
            entity.Result = GetCleanPoints(movement);

            var difference = DateTime.Now.Ticks - entity.Timestamp.Ticks;
            var differenceTimeSpan = new TimeSpan(difference);
            entity.Duration = Convert.ToDecimal(differenceTimeSpan.TotalSeconds);

            unitOfWork.MovementRepository.Add(entity);
            await unitOfWork.SaveAsync();
            return entity.Result;
        }

        private int GetCleanPoints(MovementResource movement)
        {
            var uniquePoints = new List<PositionResource>();
            var commands = mapper.Map<MovementResource, IEnumerable<ICommand>>(movement);
            var lastPosition = new PositionResource();

            foreach (var _command in commands)
            {
                var newPosition = _command.GetNewPoint(lastPosition.X, lastPosition.Y);
                
                lastPosition = newPosition ?? lastPosition;
                
                AddPoint(uniquePoints, newPosition);

            }
            return uniquePoints.Count;
        }

        private static void AddPoint(List<PositionResource> uniquePoints, PositionResource point)
        {
            if (point != null && uniquePoints.Count(r => r.Y == point.Y && r.X == point.X) == 0)
            {
                uniquePoints.Add(point);
            }
        }
    }
}
