using AutoMapper;
using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.ViewModels.Agent;
using DreamHouse.Core.Application.ViewModels.User;
using DreamHouse.Infrastructure.Identity.Entities;

namespace DreamHouse.Infrastructure.Identity.Mappings
{
    public class GeneralProfile : Profile
    {

        public GeneralProfile()
        {

            #region DTOS

            CreateMap<ApplicationUser, AuthenticationResponse>()
                    .ForMember(dest => dest.IsVerified, opt => opt.Ignore())
                    .ForMember(dest => dest.HasError, opt => opt.Ignore())
                    .ForMember(dest => dest.ErrorDescription, opt => opt.Ignore())
                    .ForMember(dest => dest.Roles, opt => opt.Ignore())
                    .ReverseMap()
                    .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                    .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                    .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                    .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                    .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                    .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                    .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                    .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                    .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                    .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                    .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore());

            //CreateMap<ApplicationUser, AgentViewModel>()
            //        .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
            //        .ForMember(dest => dest.NumberPropertiesAssigned, opt => opt.Ignore())
            //        .ForMember(dest => dest.Status, opt => opt.Ignore())
            //        .ForMember(dest => dest.UserType, opt => opt.Ignore())
            //        .ForMember(dest => dest.Roles, opt => opt.Ignore())
            //        .ReverseMap()
            //        .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
            //        .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
            //        .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
            //        .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
            //        .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
            //        .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            //        .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            //        .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            //        .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
            //        .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            //        .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore());


            //CreateMap<ApplicationUser, RegisterRequest>()
            //        .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
            //        .ForMember(dest => dest.UserType, opt => opt.Ignore())
            //        .ReverseMap()
            //        .ForMember(dest => dest.Id, opt => opt.Ignore())
            //        .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
            //        .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
            //        .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
            //        .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
            //        .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
            //        .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
            //        .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            //        .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            //        .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            //        .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
            //        .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            //        .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore());



            CreateMap<UserSaveViewModel, RegisterRequest>();
            CreateMap<RegisterRequest, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.IdCard, opt => opt.MapFrom(src => src.IdCard))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            #endregion

        }
    }
}
