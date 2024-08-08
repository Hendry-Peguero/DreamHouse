using Asp.Versioning;
using DreamHouse.Core.Application.Features.Properties.Queries.GetAllProperties;
using DreamHouse.Core.Application.ViewModels.Property;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DreamHouse.Controllers.General;

namespace WebApi.DreamHouse.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "ADMIN,DEVELOPER" )]
    public class PropertyController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PropertyViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllPropertiesQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
