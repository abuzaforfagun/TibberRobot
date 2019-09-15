using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TibberRobot.Domain.Features.RobotMovement;
using TibberRobot.Domain.Resources;

namespace TibberRobot.API.Controllers
{
    [Route("tibber-developer-test/enter-path")]
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly IRobotMovementHandler _robotMovementHandler;

        public RobotController(IRobotMovementHandler robotMovementHandler)
        {
            this._robotMovementHandler = robotMovementHandler;
        }
        public async Task<IActionResult> Post(MovementResource movement)
        {
            if (!movement.isResourceValid())
            {
                return BadRequest();
            }
            
            return Ok(await _robotMovementHandler.FindUniqueCleanedPlacesAsync(movement));
        }
    }
}
