using Asp.Versioning;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Create;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Delete;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Update;
using DreamHouse.Core.Application.Features.PropertyType.Queries.GetAllQuery;
using DreamHouse.Core.Application.Features.PropertyType.Queries.GetByIdQuery;
using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.SaleType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DreamHouse.Controllers.General;

namespace WebApi.DreamHouse.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "ADMIN,DEVELOPER")]
    public class SalesController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SaleTypeViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllSalesTypeQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetSalesTypeByIdQuery() { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaleTypeViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Post(CreateSaleTypeCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateSaleTypeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Put(int id, UpdateSaleTypeCommand command)
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(DeleteSaleTypeCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
