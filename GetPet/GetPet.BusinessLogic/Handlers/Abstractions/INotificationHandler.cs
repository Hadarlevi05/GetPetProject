using GetPet.BusinessLogic.Model;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers.Abstractions
{
    public interface INotificationHandler
    {
        Task UpsertNotification(int userId, PetFilter filter);
    }
}
