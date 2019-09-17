using System;
using System.Collections.Generic;
using AutoMapper;
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

            CreateMap<MovementResource, IEnumerable<string>>()
                .ConvertUsing<CommandsConverter>();
        }
    }

    class CommandsConverter : ITypeConverter<MovementResource, IEnumerable<string>>
    {
        public IEnumerable<string> Convert(MovementResource source, IEnumerable<string> destination, ResolutionContext context)
        {
            var result = new List<string>();
            foreach (var s in source.Commands)
            {
                for (int i = 0; i < s.Steps; i++)
                {
                    result.Add(s.Direction);
                }
            }

            return result;
        }
    }
}
