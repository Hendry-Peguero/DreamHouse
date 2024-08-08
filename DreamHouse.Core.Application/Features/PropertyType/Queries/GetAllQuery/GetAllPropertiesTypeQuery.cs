using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DreamHouse.Core.Application.Features.PropertyType.Queries.GetAllQuery
{
    public class GetAllPropertiesTypeQuery : IRequest<List<PropertyTypeViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetAllPropertiesTypeQueryHandler : IRequestHandler<GetAllPropertiesTypeQuery,List<PropertyTypeViewModel>>
    {
        private readonly IPropertyTypeRepository propertyTypeRepository;
        private readonly IMapper mapper;

        public GetAllPropertiesTypeQueryHandler(IPropertyTypeRepository propertyTypeRepository,
            IMapper mapper)
        {
            this.propertyTypeRepository = propertyTypeRepository;
            this.mapper = mapper;
        }

        public async Task<List<PropertyTypeViewModel>> Handle(GetAllPropertiesTypeQuery query, CancellationToken cancellationToken)
        {
            var propertiesVm = await GetAllViewModelWithInclude();
            if (propertiesVm == null) throw new Exception("propertyType not found");

            return propertiesVm;
        }
        private async Task<List<PropertyTypeViewModel>> GetAllViewModelWithInclude()
        {
            var propertyTypelist = await propertyTypeRepository.GetAllWithIncludeAsync(new List<string> { "Properties" });

            return propertyTypelist.Select(propertyType => new PropertyTypeViewModel
            {
                Name = propertyType.Name,
                Description = propertyType.Description,
                Id = propertyType.Id,
                CuantityPropertiesAssigned = propertyType.Properties.Count()
            }).ToList();
        }
    }

}
