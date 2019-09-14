using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TibberRobot.Domain.Resources;

namespace TibberRobot.API.Controllers
{
    [Route("tibber-developer-test/enter-path")]
    [ApiController]
    public class RobotController : ControllerBase
    {
        public async Task<IActionResult> Post(MovementResource movement)
        {
            if (!movement.isResourceValid())
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
