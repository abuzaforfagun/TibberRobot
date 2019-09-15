using System.Collections.Generic;
using TibberRobot.Domain.Features.RobotMovement;
using TibberRobot.Domain.Resources;
using Xunit;
using Moq;
using TibberRobot.Entities;
using TibberRobot.Repository.Presistance;

namespace TibberRobot.Domain.Tests
{
    public class RobotMovementTests
    {
        Mock<IMovementRepository> repositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;

        public RobotMovementTests()
        {
            repositoryMock = new Mock<IMovementRepository>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            repositoryMock.Setup(r => r.Add(It.IsAny<Movement>()));

            unitOfWorkMock.Setup(u => u.MovementRepository).Returns(repositoryMock.Object);
            unitOfWorkMock.Setup(u => u.SaveChangesAsync());
        }
        [Fact]
        public async void FindUniqueCleanedPlaces_ShouldReturn_CorrectData()
        {
            var robotMovement = new RobotMovement(unitOfWorkMock.Object);
            var resource = new MovementResource
            {
                Start = new PositionResource {X = 10, Y = 20},
                Commands = new List<CommandsResources>
                {
                    new CommandsResources {Direction = "east", Steps = 2},
                    new CommandsResources {Direction = "west", Steps = 3}
                }
            };

            var result = await robotMovement.FindUniqueCleanedPlacesAsync(resource);

            Assert.Equal(4, result);
        }

        [Fact]
        public async void FindUniqueCleanedPlaces_ShouldCall_Repository()
        {
            var robotMovement = new RobotMovement(unitOfWorkMock.Object);
            var resource = new MovementResource
            {
                Start = new PositionResource { X = 10, Y = 20 },
                Commands = new List<CommandsResources>
                {
                    new CommandsResources {Direction = "east", Steps = 2}
                }
            };

            await robotMovement.FindUniqueCleanedPlacesAsync(resource);

            repositoryMock.Verify(r => r.Add(It.IsAny<Movement>()), Times.Once);
        }

        [Fact]
        public async void FindUniqueCleanedPlaces_ShouldCall_SaveChanges()
        {
            var robotMovement = new RobotMovement(unitOfWorkMock.Object);
            var resource = new MovementResource
            {
                Start = new PositionResource { X = 10, Y = 20 },
                Commands = new List<CommandsResources>
                {
                    new CommandsResources {Direction = "east", Steps = 2}
                }
            };

            await robotMovement.FindUniqueCleanedPlacesAsync(resource);

            unitOfWorkMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
