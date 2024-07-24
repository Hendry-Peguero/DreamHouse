using Microsoft.AspNetCore.Mvc;

namespace WebApi.DreamHouse.Controllers.General
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {

    }
}
