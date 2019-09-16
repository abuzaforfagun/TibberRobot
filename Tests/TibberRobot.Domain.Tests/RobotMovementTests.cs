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
    public class RobotMovementTests
    {
        Mock<IMovementRepository> repositoryMock;
        Mock<IUnitOfWork> unitOfWorkMock;
        Mock<IMapper> mapperMock;
        #region memory data
        public static IEnumerable<object[]> ResourceList() =>
            new List<object[]>
            {
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 10, Y = 0},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "east", Steps = 1}
                        }
                    },
                    4
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 10, Y = 0},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 15},
                        }
                    },
                    10
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = -5, Y = 0},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 15},
                        }
                    },
                    0
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = -10, Y = 0},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "west", Steps = 3},
                        }
                    },
                    3
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = -10, Y = 0},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "west", Steps = 15},
                        }
                    },
                    10
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 5, Y = 0},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "west", Steps = 15},
                        }
                    },
                    0
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = 10},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "north", Steps = 3},
                            new CommandsResources {Direction = "north", Steps = 1}
                        }
                    },
                    4
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = 10},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "north", Steps = 15},
                        }
                    },
                    10
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = -5},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "north", Steps = 15},
                        }
                    },
                    0
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = -10},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "south", Steps = 3},
                        }
                    },
                    3
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = -10},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "south", Steps = 15},
                        }
                    },
                    10
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = 5},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "south", Steps = 15},
                        }
                    },
                    0
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 10, Y = 0},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "west", Steps = 1}
                        }
                    },
                    3
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = -10, Y = 0},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "west", Steps = 1}
                        }
                    },
                    1
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 10, Y = 0},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "west", Steps = 5}
                        }
                    },
                    4
                },
                new object[] {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 10, Y = 20},
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 2},
                            new CommandsResources {Direction = "west", Steps = 3}
                        }
                    },
                    3
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =0, Y =10 },
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "north", Steps = 3},
                            new CommandsResources {Direction = "south", Steps = 1}
                        }
                    }, 3
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =0, Y =10 },
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "south", Steps = 4},
                            new CommandsResources {Direction = "north", Steps = 3}
                        }
                    }, 3
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =0, Y =10 },
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "north", Steps = 3},
                            new CommandsResources {Direction = "south", Steps = 4}
                    }
                    }, 4
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =0, Y =10 },
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "north", Steps = 4},
                            new CommandsResources {Direction = "south", Steps = 4}
                    }
                    }, 5
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =-10, Y =10 },
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "north", Steps = 4},
                            new CommandsResources {Direction = "south", Steps = 4}
                        }
                    }, 5
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =10, Y =10 },
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "north", Steps = 4},
                            new CommandsResources {Direction = "south", Steps = 4}
                        }
                    }, 7
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =10, Y =10 },
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "north", Steps = 4},
                            new CommandsResources {Direction = "east", Steps = 1},
                            new CommandsResources {Direction = "south", Steps = 4}
                        }
                    }, 12
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =10, Y =10 },
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "north", Steps = 4},
                            new CommandsResources {Direction = "east", Steps = 1},
                            new CommandsResources {Direction = "west", Steps = 1},
                            new CommandsResources {Direction = "south", Steps = 4}
                        }
                    }, 8
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =10, Y =10 },
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "north", Steps = 4},
                            new CommandsResources {Direction = "east", Steps = 1},
                            new CommandsResources {Direction = "west", Steps = 1},
                            new CommandsResources {Direction = "south", Steps = 4},
                            new CommandsResources {Direction = "north", Steps = 1},
                            new CommandsResources {Direction = "west", Steps = 4}
                    }
                    }, 11
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =-5, Y =-5 },
                        Commands = new List<CommandsResources>
                        {
                            new CommandsResources {Direction = "east", Steps = 3},
                            new CommandsResources {Direction = "west", Steps = 1},
                            new CommandsResources {Direction = "south", Steps = 4},
                            new CommandsResources {Direction = "north", Steps = 1},
                            new CommandsResources {Direction = "west", Steps = 4}
                        }
                    }, 9
                },
            };
        #endregion
        public RobotMovementTests()
        {
            repositoryMock = new Mock<IMovementRepository>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            repositoryMock.Setup(r => r.Add(It.IsAny<Movement>()));
            mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MovementResource, Movement>(It.IsAny<MovementResource>()))
                .Returns(new Movement());

            unitOfWorkMock.Setup(u => u.MovementRepository).Returns(repositoryMock.Object);
            unitOfWorkMock.Setup(u => u.SaveChangesAsync());
        }
        

        [Theory]
        [MemberData(nameof(ResourceList))]
        public async void FindUniqueCleanedPlaces_ShouldReturn_CorrectData(
            MovementResource resource, int expectedResult)
        {
            var robotMovement = new RobotMovementHandler(unitOfWorkMock.Object, mapperMock.Object);

            var result = await robotMovement.FindUniqueCleanedPlacesAsync(resource);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void FindUniqueCleanedPlaces_ShouldCall_Repository()
        {
            var robotMovement = new RobotMovementHandler(unitOfWorkMock.Object, mapperMock.Object);
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
            var robotMovement = new RobotMovementHandler(unitOfWorkMock.Object, mapperMock.Object);
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
