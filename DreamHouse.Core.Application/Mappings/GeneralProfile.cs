using AutoMapper;
using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.Features.Improvements.Commands.Create;
using DreamHouse.Core.Application.Features.Improvements.Commands.Update;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Create;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Update;
using DreamHouse.Core.Application.ViewModels.Agent;
using DreamHouse.Core.Application.ViewModels.Auth;
using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Application.ViewModels.PropertyFavorite;
using DreamHouse.Core.Application.ViewModels.PropertyImage;
using DreamHouse.Core.Application.ViewModels.PropertyImprovement;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.SaleType;
using DreamHouse.Core.Application.ViewModels.User;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Authentication
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore())
                .ReverseMap();
            #endregion

            #region User

            CreateMap<UserViewModel, AuthenticationResponse>()
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore())
                .ForMember(destino => destino.IsVerified, otp => otp.Ignore())
                .ReverseMap()
                .ForMember(destino => destino.Password, otp => otp.Ignore());

            CreateMap<UserSaveViewModel, RegisterRequest>()
                .ReverseMap()
                .ForMember(destino => destino.File, otp => otp.Ignore())
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore());

            CreateMap<UserSaveViewModel, AuthenticationResponse>()
                .ReverseMap()
                .ForMember(destino => destino.File, otp => otp.Ignore())
                .ForMember(destino => destino.ConfirmPassword, otp => otp.Ignore());

            CreateMap<UserViewModel, AgentViewModel>()
                .ForMember(destino => destino.NumberPropertiesAssigned, otp => otp.Ignore())
                .ReverseMap();

            CreateMap<UserViewModel, AgentViewModel>();
            CreateMap<UserSaveViewModel, AgentViewModel>();

            #endregion

            #region Property

            CreateMap<PropertyEntity, PropertyViewModel>()
                .ForMember(destino => destino.MarkedAsFavorite, otp => otp.Ignore())
                .ReverseMap();

            CreateMap<PropertyEntity, PropertySaveViewModel>()
                .ForMember(destino => destino.IdSelectedImprovements, otp => otp.Ignore())
                .ForMember(destino => destino.Images, otp => otp.Ignore())
                .ForMember(destino => destino.ImagesUrl, otp => otp.Ignore())
                .ForMember(destino => destino.PropertyTypes, otp => otp.Ignore())
                .ForMember(destino => destino.SaleTypes, otp => otp.Ignore())
                .ForMember(destino => destino.Improvements, otp => otp.Ignore())
                .ReverseMap()
                .ForMember(destino => destino.TypeProperty, otp => otp.Ignore())
                .ForMember(destino => destino.TypeSale, otp => otp.Ignore())
                .ForMember(destino => destino.Favorites, otp => otp.Ignore())
                .ForMember(destino => destino.Images, otp => otp.Ignore())
                .ForMember(destino => destino.ImprovementProperties, otp => otp.Ignore());

            #endregion

            #region PropertyFavoriteViewModel

            CreateMap<PropertyFavoriteEntity, PropertyFavoriteViewModel>()
                .ReverseMap();

            #endregion

            #region PropertyImageViewModel

            CreateMap<PropertyImageEntity, PropertyImageViewModel>()
                .ReverseMap();

            #endregion

            #region PropertyImprovementViewModel

            CreateMap<PropertyImprovementEntity, PropertyImprovementViewModel>()
                .ReverseMap();

            #endregion

            #region PropertyType
            CreateMap<PropertyTypeEntity, PropertyTypeViewModel>()
                .ForMember(destino => destino.CuantityPropertiesAssigned, otp => otp.Ignore())
                .ReverseMap();

            CreateMap<PropertyTypeEntity, PropertyTypeSaveViewModel>()
                .ReverseMap()
                .ForMember(destino => destino.Properties, otp => otp.Ignore());

            #endregion

            #region SalesType
            CreateMap<SaleTypeEntity, SaleTypeViewModel>()
                .ForMember(destino => destino.CuantitySalesAssigned, otp => otp.Ignore())
                .ReverseMap();

            CreateMap<SaleTypeEntity, SaleTypeSaveViewModel>().ReverseMap();

            #endregion

            #region Improvements
            CreateMap<ImprovementEntity, ImprovementViewModel>()
                .ReverseMap();

            CreateMap<ImprovementEntity, ImprovementSaveViewModel>()
                .ReverseMap()
                .ForMember(destino => destino.ImprovementProperties, otp => otp.Ignore());

            #endregion

            #region PropertyTypeCommand
            CreateMap<PropertyTypeEntity, CreatePropertyTypeCommand>()
                .ReverseMap()
                .ForMember(destino => destino.Id, otp => otp.Ignore())
                .ForMember(destino => destino.Properties, otp => otp.Ignore());

            CreateMap<PropertyTypeEntity, UpdatePropertyTypeCommand>()
                .ReverseMap()
                .ForMember(destino => destino.Properties, otp => otp.Ignore());

            CreateMap<PropertyTypeEntity, UpdatePropertyTypeResponse>()
                .ReverseMap()
                .ForMember(destino => destino.Properties, otp => otp.Ignore());

            CreateMap<PropertyTypeEntity, PropertyFavoriteViewModel>()
                .ReverseMap();
            #endregion

            #region ImprovementCommand
            CreateMap<ImprovementEntity, CreateImprovementCommand>()
                .ReverseMap()
                .ForMember(destino => destino.Id, otp => otp.Ignore())
                .ForMember(destino => destino.ImprovementProperties, otp => otp.Ignore());

            CreateMap<ImprovementEntity, UpdateImprovementCommand>()
                .ReverseMap()
                .ForMember(destino => destino.ImprovementProperties, otp => otp.Ignore());

            CreateMap<ImprovementEntity, PropertyFavoriteViewModel>()
                .ReverseMap();

            // Aquí es donde añadimos el nuevo mapeo
            CreateMap<ImprovementEntity, UpdateImprovementResponse>();
            #endregion

            #region SalesTypeQuery
            CreateMap<SaleTypeEntity, CreateSaleTypeCommand>()
                .ReverseMap()
                .ForMember(destino => destino.Properties, otp => otp.Ignore())
                .ForMember(destino => destino.Id, otp => otp.Ignore());

            CreateMap<SaleTypeEntity, UpdateSaleTypeCommand>()
                .ReverseMap()
                .ForMember(destino => destino.Properties, otp => otp.Ignore());


            CreateMap<SaleTypeEntity, UpdateSaleTypeResponse>()
                .ReverseMap()
                .ForMember(destino => destino.Properties, otp => otp.Ignore());


            #endregion

        }
    }
}
