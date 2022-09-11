using Application.Features.Frameworks.Commands.CreateFamework;
using Application.Features.Frameworks.Commands.DeleteFramework;
using Application.Features.Frameworks.Commands.UpdateFramework;
using Application.Features.Frameworks.Dtos;
using Application.Features.Frameworks.Models;
using Application.Features.Frameworks.Queries.GetByIdFramework;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Frameworks.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Framework, CreateFrameworkDto>().ReverseMap();
            CreateMap<Framework, CreateFrameworkCommand>().ReverseMap();
            CreateMap<Framework, DeleteFrameworkDto>().ReverseMap();
            CreateMap<Framework, DeleteFrameworkCommand>().ReverseMap();
            CreateMap<Framework, UpdateFrameworkDto>().ReverseMap();
            CreateMap<Framework, UpdateFrameworkCommand>().ReverseMap();

            CreateMap<Framework, FrameworkListDto>()
                .ForMember(f => f.ProgrammingLanguageName,
                    opt => opt.MapFrom(c => c.ProgrammingLanguage.Name))
                .ReverseMap();
            CreateMap<IPaginate<Framework>, FrameworkListModel>().ReverseMap();

            CreateMap<Framework, GetByIdFrameworkDto>()
                .ForMember(f => f.ProgrammingLanguageName, opt => opt.MapFrom(g => g.ProgrammingLanguage.Name)).ReverseMap();
            CreateMap<Framework, GetByIdFrameworkQuery>().ReverseMap();
        }
    }
}