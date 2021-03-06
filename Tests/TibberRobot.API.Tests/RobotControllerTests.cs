using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TibberRobot.API.Controllers;
using TibberRobot.Domain.Features.RobotMovement;
using TibberRobot.Domain.Resources;
using Xunit;

namespace TibberRobot.API.Tests
{
    public class RobotControllerTests
    {
        RobotController controller;

        public RobotControllerTests()
        {
            controller = new RobotController(Mock.Of<IRobotMovementHandler>());
        }

        [Fact]
        public async void Post_ShoulReturn_OkResponse_For_CorrectData()
        {
            
            var commands = new List<CommandResource>
            {
                new CommandResource() {Direction = "east", Steps = 1}
            };
            var query = new MovementResource {Commands = commands, Start = new PositionResource()};

            var result = await controller.Post(query);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Post_ShouldReturn_BadRequest_For_Empty_Command_And_Start_Data()
        {
            var result = await controller.Post(new MovementResource());

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Post_ShouldReturn_BadRequest_For_Empty_CommandsData()
        {
            var query = new MovementResource { Start = new PositionResource() };

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Post_ShouldReturn_BadRequest_For_Empty_StartData()
        {
            var query = new MovementResource { Commands = new List<CommandResource>()};

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Post_ShouldReturn_BadRequest_For_NoCommands()
        {
            var query = new MovementResource { Commands = new List<CommandResource>(), Start = new PositionResource() };

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [MemberData(nameof(InvalidSteps))]
        public async void Post_ShouldReturn_BadRequest_For_Invalid_Steps(int steps)
        {
            var commands = new List<CommandResource>
            {
                new CommandResource() { Direction = "east", Steps = steps }
            };
            var query = new MovementResource { Commands = commands, Start = new PositionResource() };

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Post_ShouldReturn_BadRequest_For_Invalid_Direction()
        {
            var commands = new List<CommandResource>
            {
                new CommandResource() { Direction = "abc", Steps = 1 }
            };
            var query = new MovementResource { Commands = commands, Start = new PositionResource() };

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [MemberData(nameof(InvalidStarts))]
        public async void Post_ShouldReturn_BadRequest_For_Invalid_Start(int x, int y)
        {
            var commands = new List<CommandResource>
            {
                new CommandResource() { Direction = "west", Steps = 1 }
            };
            var position = new PositionResource( x, y );
            var query = new MovementResource { Commands = commands, Start = position };

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Post_ShouldCall_RobotMovementHandler()
        {
            var handler = new Mock<IRobotMovementHandler>();
            handler.Setup(f => f.HandleAsync(It.IsAny<MovementResource>())).ReturnsAsync(It.IsAny<int>());
            var commands = new List<CommandResource>
            {
                new CommandResource() {Direction = "east", Steps = 1}
            };
            var query = new MovementResource { Commands = commands, Start = new PositionResource() };
            var controller = new RobotController(handler.Object);

            await controller.Post(query);

            handler.Verify(f => f.HandleAsync(query), Times.Once);

        }

        public static IEnumerable<object[]> InvalidSteps() =>
            new List<object[]>
            {
                new object[] {-1},
                new object[] {0},
                new object[] { 100001 }
            };

        public static IEnumerable<object[]> InvalidStarts() =>
            new List<object[]>
            {
                new object[] { 100001, 100001 },
                new object[] { -100001, -100001 },
                new object[] { 0, 100001 },
                new object[] { 0, -100001 },
                new object[] { 100001, 1 },
                new object[] { -100001, 1 }
            };
    }
}
