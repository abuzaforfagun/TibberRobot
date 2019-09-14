using System.Collections.Generic;
using TibberRobot.Domain.Features;
using TibberRobot.Domain.Resources;
using Xunit;

namespace TibberRobot.Domain.Tests
{
    public class RobotMovementTests
    {
        [Fact]
        public void FindUniqueCleanedPlaces_ShouldReturn_CorrectData()
        {
            var robotMovement = new RobotMovement();
            var resource = new MovementResource
            {
                Start = new PositionResource {X = 10, Y = 20},
                Commands = new List<CommandsResources>
                {
                    new CommandsResources {Direction = "east", Steps = 2},
                    new CommandsResources {Direction = "west", Steps = 3}
                }
            };

            var result = robotMovement.FindUniqueCleanedPlaces(resource);

            Assert.Equal(4, result);
        }
    }
}
