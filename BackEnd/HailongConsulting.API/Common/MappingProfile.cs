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

        // 附件映射
        CreateMap<Attachment, AttachmentDto>();
        CreateMap<UploadAttachmentDto, Attachment>();

        // 公告映射
        CreateMap<Announcement, AnnouncementDto>()
            .ForMember(dest => dest.IsTop, opt => opt.MapFrom(src => src.IsTop == 1))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.AttachmentIds, opt => opt.MapFrom(src => DeserializeUintList(src.AttachmentIds)));
        
        CreateMap<CreateAnnouncementDto, Announcement>()
            .ForMember(dest => dest.IsTop, opt => opt.MapFrom(src => (sbyte)(src.IsTop ? 1 : 0)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (sbyte)src.Status))
            .ForMember(dest => dest.AttachmentIds, opt => opt.MapFrom(src => SerializeUintList(src.AttachmentIds)));
        
        CreateMap<UpdateAnnouncementDto, Announcement>()
            .ForMember(dest => dest.IsTop, opt => opt.MapFrom(src => src.IsTop.HasValue ? (sbyte)(src.IsTop.Value ? 1 : 0) : (sbyte?)null))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.HasValue ? (sbyte)src.Status.Value : (sbyte?)null))
            .ForMember(dest => dest.AttachmentIds, opt => opt.MapFrom(src => SerializeUintList(src.AttachmentIds)))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // 信息发布映射
        CreateMap<InfoPublication, InfoPublicationDto>()
            .ForMember(dest => dest.IsTop, opt => opt.MapFrom(src => src.IsTop == 1))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.AttachmentIds, opt => opt.MapFrom(src => DeserializeUintList(src.AttachmentIds)));
        
        CreateMap<CreateInfoPublicationDto, InfoPublication>()
            .ForMember(dest => dest.IsTop, opt => opt.MapFrom(src => (sbyte)(src.IsTop ? 1 : 0)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (sbyte)src.Status))
            .ForMember(dest => dest.AttachmentIds, opt => opt.MapFrom(src => SerializeUintList(src.AttachmentIds)));
        
        CreateMap<UpdateInfoPublicationDto, InfoPublication>()
            .ForMember(dest => dest.IsTop, opt => opt.MapFrom(src => src.IsTop.HasValue ? (sbyte)(src.IsTop.Value ? 1 : 0) : (sbyte?)null))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.HasValue ? (sbyte)src.Status.Value : (sbyte?)null))
            .ForMember(dest => dest.AttachmentIds, opt => opt.MapFrom(src => SerializeUintList(src.AttachmentIds)))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

    // 辅助方法：序列化和反序列化 JSON
    private static string? SerializeUintList(List<uint>? list)
    {
        if (list == null || list.Count == 0)
            return null;
        return System.Text.Json.JsonSerializer.Serialize(list);
    }

    private static List<uint>? DeserializeUintList(string? json)
    {
        if (string.IsNullOrEmpty(json))
            return null;
        return System.Text.Json.JsonSerializer.Deserialize<List<uint>>(json);
    }
}