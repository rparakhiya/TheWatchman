using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TheWatchman.Server.Entities;
using TheWatchman.Server.Models;

namespace TheWatchman.Server.MappingProfiles
{
    public class MonitoredResourceProfile : Profile
    {
        public MonitoredResourceProfile()
        {
            CreateMap<MonitoredResource, MonitoredResourceModel>();

            CreateMap<MonitoredResource, ResourceStatus>()
                .ForMember(dest => dest.Resource, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Status, opt => opt.MapFrom<ResourceStatusResolver>());
        }
    }
}
