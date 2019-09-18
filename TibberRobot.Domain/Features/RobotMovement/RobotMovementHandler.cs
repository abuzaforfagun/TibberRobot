using System;
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
        private readonly IRobotMovementHelper helper;

        public RobotMovementHandler(IUnitOfWork unitOfWork, IMapper mapper, IRobotMovementHelper helper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.helper = helper;
        }
        public async Task<int> HandleAsync(MovementResource movement)
        {
            var entity = mapper.Map<MovementResource, Movement>(movement);
            entity.Result = helper.GetCleanPoints(movement);

            var difference = DateTime.Now.Ticks - entity.Timestamp.Ticks;
            var differenceTimeSpan = new TimeSpan(difference);
            entity.Duration = Convert.ToDecimal(differenceTimeSpan.TotalSeconds);

            unitOfWork.MovementRepository.Add(entity);
            await unitOfWork.SaveAsync();
            return entity.Result;
        }
    }
}
