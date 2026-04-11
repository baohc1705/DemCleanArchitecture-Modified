using AutoMapper;
using DemoCleanArchitectureV2.Application.Common.DTOs;
using DemoCleanArchitectureV2.Application.Features.Menus.Commands.CreateMenu;
using DemoCleanArchitectureV2.Application.Features.Menus.Commands.UpdateMenu;
using DemoCleanArchitectureV2.Application.Features.News.Commands.CreateNews;
using DemoCleanArchitectureV2.Domain.Entities;

namespace DemoCleanArchitectureV2.Application.Common.Mappings
{
    public class ApplicationMappingsProfile : Profile
    {
        public ApplicationMappingsProfile()
        {
            CreateMap<CreateMenuCommand, Menu>().ReverseMap();

            CreateMap<UpdateMenuCommand, Menu>().ReverseMap();

            CreateMap<Menu, MenuDto>()
                .ForMember(dest => dest.News, opt => opt.MapFrom(src => src.NewsMenu.Select(mn => mn.News).ToList()))
                .ReverseMap();
            CreateMap<News, NewsDto>()
                .ForMember(dest => dest.Menus, opt => opt.MapFrom(src => src.NewsMenu.Select(mn => mn.Menu).ToList()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();

            CreateMap<CreateNewsCommand, News>().ReverseMap();

            CreateMap<UpdateMenuCommand, News>().ReverseMap();

        }
    }
}
