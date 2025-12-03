using AutoMapper;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Models.Entities;

namespace HailongConsulting.API.Common;

/// <summary>
/// AutoMapper映射配置
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Project映射
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.ClientName : null))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null))
            .ForMember(dest => dest.ProjectManagerName, opt => opt.MapFrom(src => src.ProjectManager != null ? src.ProjectManager.FullName : null));

        CreateMap<CreateProjectDto, Project>();
        CreateMap<UpdateProjectDto, Project>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // User映射
        CreateMap<User, LoginResponseDto>();

        // 系统配置映射
        // 轮播图
        CreateMap<CarouselBanner, CarouselBannerDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status == 1));
        CreateMap<CreateCarouselBannerDto, CarouselBanner>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (sbyte)(src.Status ? 1 : 0)));

        // 企业简介
        CreateMap<CompanyProfile, CompanyProfileDto>();
        CreateMap<UpdateCompanyProfileDto, CompanyProfile>();

        // 重要业绩
        CreateMap<MajorAchievement, MajorAchievementDto>();
        CreateMap<CreateMajorAchievementDto, MajorAchievement>();

        // 友情链接
        CreateMap<FriendlyLink, FriendlyLinkDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status == 1));
        CreateMap<CreateFriendlyLinkDto, FriendlyLink>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (sbyte)(src.Status ? 1 : 0)));
    }
}