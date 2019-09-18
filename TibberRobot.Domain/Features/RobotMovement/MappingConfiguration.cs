using System;
using System.Collections.Generic;
using AutoMapper;
using TibberRobot.Domain.Features.RobotMovement.Commands;
using TibberRobot.Domain.Resources;
using TibberRobot.Entities;

namespace TibberRobot.Domain.Features.RobotMovement
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<MovementResource, Movement>()
                .ForMember(d => d.Timestamp, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(d => d.Commands, opt => opt.MapFrom(o => o.Commands.Count))
                .ForMember(d => d.Duration, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Result, opt => opt.Ignore());

            CreateMap<MovementResource, IEnumerable<ICommand>>()
                .ConvertUsing<CommandsConverter>();
        }
    }

    class CommandsConverter : ITypeConverter<MovementResource, IEnumerable<ICommand>>
    {
        public IEnumerable<ICommand> Convert(MovementResource source, IEnumerable<ICommand> destination, ResolutionContext context)
        {
            var result = new List<ICommand>();
            foreach (var s in source.Commands)
            {
                for (int i = 0; i < s.Steps; i++)
                {
                    if (s.Direction == "east")
                    {
                        result.Add(new EastCommand
                        {
                            Limit = source.Start.X
                        });
                    }
                    else if (s.Direction == "west")
                    {
                        result.Add(new WestCommand
                        {
                            Limit = source.Start.X
                        });
                    }
                    else if (s.Direction == "north")
                    {
                        result.Add(new NorthCommand
                        {
                            Limit = source.Start.Y
                        });
                    }
                    else if (s.Direction == "south")
                    {
                        result.Add(new SouthCommand
                        {
                            Limit = source.Start.Y
                        });
                    }
                }
            }

            return result;
        }
    }
}
