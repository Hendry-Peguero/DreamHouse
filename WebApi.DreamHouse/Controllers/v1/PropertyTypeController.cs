using Asp.Versioning;
using DreamHouse.Core.Application.Features.Properties.Queries.GetAllProperties;
using DreamHouse.Core.Application.Features.Properties.Queries.GetPropertyById;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Create;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Update;
using DreamHouse.Core.Application.Features.PropertyType.Queries.GetAllQuery;
using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DreamHouse.Controllers.General;

namespace WebApi.DreamHouse.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "ADMIN,DEVELOPER" )]
    public class PropertyTypeController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PropertyViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllPropertiesTypeQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PropertyTypeViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Post(CreatePropertyTypeCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdatePropertyTypeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Put(int id, UpdatePropertyTypeCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (id != command.Id)
                    return BadRequest();

                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
