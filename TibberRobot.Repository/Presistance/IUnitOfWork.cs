using System.Threading.Tasks;
using TibberRobot.Domain.Features.RobotMovement;

namespace TibberRobot.Repository.Presistance
{
    public interface IUnitOfWork
    {
        IMovementRepository MovementRepository { get; set; }

        Task SaveChangesAsync();
    }
}
