using TibberRobot.Domain.Features.RobotMovement.Commands;
using Xunit;

namespace TibberRobot.Domain.Tests
{
    public class CommandsTests
    {
        [Fact]
        public void NorthCommand_GetNewPoint_ShouldReturn_Null_ForInvalidPosition()
        {
            var command = new NorthCommand() {Limit = 10};

            var data = command.GetNewPoint(10, 10);
            Assert.Null(data);
        }

        [Fact]
        public void NorthCommand_GetNewPoint_ShouldReturn_CorrectData_ForValidPosition()
        {
            var command = new NorthCommand() { Limit = 10 };

            var data = command.GetNewPoint(10, 5);
            Assert.NotNull(data);
            Assert.Equal(10, data.X);
            Assert.Equal(6, data.Y);
        }

        [Fact]
        public void SouthCommand_GetNewPoint_ShouldReturn_Null_ForInvalidPosition()
        {
            var command = new SouthCommand() { Limit = 10 };

            var data = command.GetNewPoint(10, 0);
            Assert.Null(data);
        }

        [Fact]
        public void SouthCommand_GetNewPoint_ShouldReturn_CorrectData_ForValidPosition()
        {
            var command = new SouthCommand() { Limit = 10 };

            var data = command.GetNewPoint(9, 5);
            Assert.NotNull(data);
            Assert.Equal(9, data.X);
            Assert.Equal(4, data.Y);
        }

        [Fact]
        public void EastCommand_GetNewPoint_ShouldReturn_Null_ForInvalidPosition()
        {
            var command = new NorthCommand() { Limit = -10 };

            var data = command.GetNewPoint(10, 10);
            Assert.Null(data);
        }

        [Fact]
        public void EastCommand_GetNewPoint_ShouldReturn_CorrectData_ForValidPosition()
        {
            var command = new EastCommand() { Limit = 10 };

            var data = command.GetNewPoint(9, 5);
            Assert.NotNull(data);
            Assert.Equal(10, data.X);
            Assert.Equal(5, data.Y);
        }

        [Fact]
        public void WestCommand_GetNewPoint_ShouldReturn_Null_ForInvalidPosition()
        {
            var command = new WestCommand() { Limit = 10 };

            var data = command.GetNewPoint(0, 10);
            Assert.Null(data);
        }

        [Fact]
        public void WestCommand_GetNewPoint_ShouldReturn_CorrectData_ForValidPosition()
        {
            var command = new WestCommand() { Limit = 10 };

            var data = command.GetNewPoint(9, 5);
            Assert.NotNull(data);
            Assert.Equal(8, data.X);
            Assert.Equal(5, data.Y);
        }
    }
}
