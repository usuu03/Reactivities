using System;
using Application.Activities.DTO;
using Application.Activities.DTOs;
using AutoMapper;
using Domain;

namespace Application.Common;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // CreateMap<Source, Destination>();
        // Example: Create a mapping from Activity to Activity
        // This is a self-mapping, which means the source and destination types are the same
        CreateMap<Activity, Activity>();
        CreateMap<CreateActivityDTO, Activity>();
        CreateMap<EditActivityDto, Activity>();
    }
}
