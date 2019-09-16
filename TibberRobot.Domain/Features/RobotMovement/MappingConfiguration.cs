using System;
using System.Collections.Generic;
using System.Text;
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
        }
    }
}
