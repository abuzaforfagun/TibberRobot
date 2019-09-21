using AutoMapper;
using System.Collections.Generic;
using TibberRobot.Domain.Features.RobotMovement;
using TibberRobot.Domain.Resources;
using Xunit;

namespace TibberRobot.Domain.Tests
{
    public class RobotMovementHelperTests
    {
        [Theory]
        [MemberData(nameof(ResourceList))]
        public void GetCleanPoints_ShouldReturn_CorrectData(MovementResource resource, int expectedResult)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingConfiguration());
            });
            var mapper = mockMapper.CreateMapper();
            var helper = new RobotMovementHelper(mapper);

            var result = helper.GetCleanPoints(resource);

            Assert.Equal(expectedResult, result);
        }

        #region memory data
        public static IEnumerable<object[]> ResourceList() =>
            new List<object[]>
            {
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 10, Y = 0},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "east", Steps = 1}
                        }
                    },
                    5
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 10, Y = 0},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 15},
                        }
                    },
                    11
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = -5, Y = 0},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 15},
                        }
                    },
                    1
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = -10, Y = 0},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "west", Steps = 3},
                        }
                    },
                    4
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = -10, Y = 0},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "west", Steps = 15},
                        }
                    },
                    11
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 5, Y = 0},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "west", Steps = 15},
                        }
                    },
                    1
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = 10},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "north", Steps = 3},
                            new CommandResource {Direction = "north", Steps = 1}
                        }
                    },
                    5
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = 10},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "north", Steps = 15},
                        }
                    },
                    11
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = -5},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "north", Steps = 15},
                        }
                    },
                    1
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = -10},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "south", Steps = 3},
                        }
                    },
                    4
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = -10},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "south", Steps = 15},
                        }
                    },
                    11
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 0, Y = 5},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "south", Steps = 15},
                        }
                    },
                    1
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 10, Y = 0},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "west", Steps = 1}
                        }
                    },
                    4
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = -10, Y = 0},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "west", Steps = 1}
                        }
                    }, 2
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 10, Y = 0},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "west", Steps = 5}
                        }
                    }, 4
                },
                new object[] {
                    new MovementResource
                    {
                        Start = new PositionResource {X = 10, Y = 20},
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 2},
                            new CommandResource {Direction = "west", Steps = 3}
                        }
                    }, 3
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =0, Y =10 },
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "north", Steps = 3},
                            new CommandResource {Direction = "south", Steps = 1}
                        }
                    }, 4
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =0, Y =10 },
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "south", Steps = 4},
                            new CommandResource {Direction = "north", Steps = 3}
                        }
                    }, 4
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =0, Y =10 },
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "north", Steps = 3},
                            new CommandResource {Direction = "south", Steps = 4}
                    }
                    }, 4
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =0, Y =10 },
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "north", Steps = 4},
                            new CommandResource {Direction = "south", Steps = 4}
                    }
                    }, 5
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =-10, Y =10 },
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "north", Steps = 4},
                            new CommandResource {Direction = "south", Steps = 4}
                        }
                    }, 5
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =10, Y =10 },
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "north", Steps = 4},
                            new CommandResource {Direction = "south", Steps = 4}
                        }
                    }, 8
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =10, Y =10 },
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "north", Steps = 4},
                            new CommandResource {Direction = "east", Steps = 1},
                            new CommandResource {Direction = "south", Steps = 4}
                        }
                    }, 13
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =10, Y =10 },
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "north", Steps = 4},
                            new CommandResource {Direction = "east", Steps = 1},
                            new CommandResource {Direction = "west", Steps = 1},
                            new CommandResource {Direction = "south", Steps = 4}
                        }
                    }, 9
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =10, Y =10 },
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "north", Steps = 4},
                            new CommandResource {Direction = "east", Steps = 1},
                            new CommandResource {Direction = "west", Steps = 1},
                            new CommandResource {Direction = "south", Steps = 4},
                            new CommandResource {Direction = "north", Steps = 1},
                            new CommandResource {Direction = "west", Steps = 4}
                    }
                    }, 12
                },
                new object[]
                {
                    new MovementResource
                    {
                        Start = new PositionResource {X =-5, Y =-5 },
                        Commands = new List<CommandResource>
                        {
                            new CommandResource {Direction = "east", Steps = 3},
                            new CommandResource {Direction = "west", Steps = 1},
                            new CommandResource {Direction = "south", Steps = 4},
                            new CommandResource {Direction = "north", Steps = 1},
                            new CommandResource {Direction = "west", Steps = 4}
                        }
                    }, 10
                },
            };
        #endregion
    }
}
