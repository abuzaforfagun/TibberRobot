using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TibberRobot.Domain.Features;
using TibberRobot.Domain.Resources;

namespace TibberRobot.API.Controllers
{
    [Route("tibber-developer-test/enter-path")]
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly IRobotMovement robotMovement;

        public RobotController(IRobotMovement robotMovement)
        {
            this.robotMovement = robotMovement;
        }
        public async Task<IActionResult> Post(MovementResource movement)
        {
            if (!movement.isResourceValid())
            {
                return BadRequest();
            }
            
            return Ok(robotMovement.FindUniqueCleanedPlaces(movement));
        }
    }
}
