using System.Collections.Generic;
using TibberRobot.Domain.Features.RobotMovement;
using TibberRobot.Domain.Resources;
using Xunit;
using Moq;
using TibberRobot.Entities;
using TibberRobot.Repository.Presistance;
using AutoMapper;

namespace TibberRobot.Domain.Tests
{
    public class RobotMovementHandlerTests
    {
        private readonly Mock<IMovementRepository> repositoryMock;
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IRobotMovementHelper> helperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly RobotMovementHandler handler;

        public RobotMovementHandlerTests()
        {
            repositoryMock = new Mock<IMovementRepository>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            repositoryMock.Setup(r => r.Add(It.IsAny<Movement>()));
            mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MovementResource, Movement>(It.IsAny<MovementResource>()))
                .Returns(new Movement());
            helperMock = new Mock<IRobotMovementHelper>();
            helperMock.Setup(h => h.GetCleanPoints(It.IsAny<MovementResource>())).Returns(It.IsAny<int>());

            unitOfWorkMock.Setup(u => u.MovementRepository).Returns(repositoryMock.Object);
            unitOfWorkMock.Setup(u => u.SaveAsync());

            handler = new RobotMovementHandler(unitOfWorkMock.Object, mapperMock.Object, helperMock.Object);

        }

        [Fact]
        public async void HandleAsync_ShouldCall_RobotMovementHelper()
        {
            var resource = new MovementResource
            {
                Start = new PositionResource { X = 10, Y = 20 },
                Commands = new List<CommandResource>
                {
                    new CommandResource {Direction = "east", Steps = 2}
                }
            };

            await handler.HandleAsync(resource);

            helperMock.Verify(h => h.GetCleanPoints(resource), Times.Once);
        }

        [Fact]
        public async void HandleAsync_ShouldCall_Repository()
        {
            var resource = new MovementResource
            {
                Start = new PositionResource { X = 10, Y = 20 },
                Commands = new List<CommandResource>
                {
                    new CommandResource {Direction = "east", Steps = 2}
                }
            };

            await handler.HandleAsync(resource);

            repositoryMock.Verify(r => r.Add(It.IsAny<Movement>()), Times.Once);
        }

        [Fact]
        public async void HandleAsync_ShouldCall_SaveChanges()
        {
            var resource = new MovementResource
            {
                Start = new PositionResource { X = 10, Y = 20 },
                Commands = new List<CommandResource>
                {
                    new CommandResource {Direction = "east", Steps = 2}
                }
            };

            await handler.HandleAsync(resource);

            unitOfWorkMock.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}
