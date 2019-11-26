using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatrGrapQL.Resolvers;

namespace MediatrGrapQL.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Property([FromQuery] PropertiesResolver query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
