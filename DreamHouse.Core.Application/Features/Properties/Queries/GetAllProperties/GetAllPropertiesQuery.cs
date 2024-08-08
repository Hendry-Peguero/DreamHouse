using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.ViewModels.Property;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQuery : IRequest<List<PropertyViewModel>>
    {
    }

    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, List<PropertyViewModel>>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;

        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository , IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<List<PropertyViewModel>> Handle(GetAllPropertiesQuery query, CancellationToken cancellationToken)
        {

            var propertiesVm = mapper.Map<List<PropertyViewModel>>(await propertyRepository.GetAllAsync());
            if (propertiesVm == null) throw new Exception("There are not categories");

            return propertiesVm;
        }
    }

}
