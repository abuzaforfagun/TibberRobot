using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using TibberRobot.Domain.Features.RobotMovement.Commands;
using TibberRobot.Domain.Resources;

namespace TibberRobot.Domain.Features.RobotMovement
{
    public class RobotMovementHelper : IRobotMovementHelper
    {
        private readonly IMapper mapper;

        public RobotMovementHelper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public int GetCleanPoints(MovementResource movement)
        {
            var uniquePoints = new HashSet<PositionResource>();
            uniquePoints.Add(new PositionResource(0, 0));
            var commands = mapper.Map<MovementResource, IEnumerable<ICommand>>(movement);
            var lastPosition = new PositionResource();

            foreach (var _command in commands)
            {
                var newPosition = _command.GetNewPoint(lastPosition.X, lastPosition.Y);

                lastPosition = newPosition ?? lastPosition;
                uniquePoints.Add(lastPosition);
            }

            return uniquePoints.Count();
        }
    }
}
