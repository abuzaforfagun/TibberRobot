using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TibberRobot.API.Controllers;
using TibberRobot.Domain.Enums;
using TibberRobot.Domain.Resources;
using Xunit;

namespace TibberRobot.API.Tests
{
    public class RobotControllerTests
    {
        [Fact]
        public async void Post_ShoulReturn_OkResponse_For_CorrectData()
        {
            var controller = new RobotController();
            var commands = new List<CommandsResources>
            {
                new CommandsResources() {Direction = "east", Steps = 1}
            };
            var query = new MovementResource {Commands = commands, Start = new PositionResource()};

            var result = await controller.Post(query);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void Post_ShouldReturn_BadRequest_For_Empty_Command_And_Start_Data()
        {
            var controlelr = new RobotController();

            var result = await controlelr.Post(new MovementResource());

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Post_ShouldReturn_BadRequest_For_Empty_CommandsData()
        {
            var controller = new RobotController();
            var query = new MovementResource { Start = new PositionResource() };

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Post_ShouldReturn_BadRequest_For_Empty_StartData()
        {
            var controller = new RobotController();
            var query = new MovementResource { Commands = new List<CommandsResources>()};

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Post_ShouldReturn_BadRequest_For_NoCommands()
        {
            var controller = new RobotController();
            var query = new MovementResource { Commands = new List<CommandsResources>(), Start = new PositionResource() };

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [MemberData(nameof(InvalidSteps))]
        public async void Post_ShouldReturn_BadRequest_For_Invalid_Steps(int steps)
        {
            var controller = new RobotController();
            var commands = new List<CommandsResources>
            {
                new CommandsResources() { Direction = "east", Steps = steps }
            };
            var query = new MovementResource { Commands = commands, Start = new PositionResource() };

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Post_ShouldReturn_BadRequest_For_Invalid_Direction()
        {
            var controller = new RobotController();
            var commands = new List<CommandsResources>
            {
                new CommandsResources() { Direction = "abc", Steps = 1 }
            };
            var query = new MovementResource { Commands = commands, Start = new PositionResource() };

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [MemberData(nameof(InvalidStarts))]
        public async void Post_ShouldReturn_BadRequest_For_Invalid_Start(int x, int y)
        {
            var controller = new RobotController();
            var commands = new List<CommandsResources>
            {
                new CommandsResources() { Direction = "west", Steps = 1 }
            };
            var position = new PositionResource {X = x, Y = y};
            var query = new MovementResource { Commands = commands, Start = position };

            var result = await controller.Post(query);

            Assert.IsType<BadRequestResult>(result);
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
