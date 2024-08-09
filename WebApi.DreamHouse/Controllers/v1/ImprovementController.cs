using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DreamHouse.Controllers.General;
using DreamHouse.Core.Application.Features.Improvements.Commands.Create;
using DreamHouse.Core.Application.Features.Improvements.Commands.Delete;
using DreamHouse.Core.Application.Features.Improvements.Commands.Update;
using DreamHouse.Core.Application.Features.Improvements.Queries.GetAllQuery;
using DreamHouse.Core.Application.Features.Improvements.Queries.GetByIdQuery;
using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Application.ViewModels.PropertyType;

namespace WebApi.DreamHouse.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "ADMIN,DEVELOPER")]
    public class ImprovementController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ImprovementViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllImprovementQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImprovementViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetImprovementsByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ImprovementViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateImprovementCommand command)
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

        [Authorize(Roles = "ADMIN")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateImprovementResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateImprovementCommand command)
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

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteImprovementCommand { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}