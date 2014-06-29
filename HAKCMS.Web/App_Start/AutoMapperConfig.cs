using AutoMapper;
using HAKCMS.Data.Identity;
using HAKCMS.Model.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HAKCMS.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<ApplicationUser, UserViewModel>()
            .ForMember(dest => dest.Roles, opts => opts.Ignore());
            //.ForMember(dest => dest.SelectedRoleIds, opts => opts.Ignore());

            Mapper.CreateMap<UserViewModel, ApplicationUser>()
            .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.Roles))
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.UserName))
            .ForAllMembers(opt => opt.Ignore());

            Mapper.CreateMap<RoleViewModel, IdentityRole>();
            
            Mapper.CreateMap<IdentityRole, RoleViewModel>();

        }
    }
}