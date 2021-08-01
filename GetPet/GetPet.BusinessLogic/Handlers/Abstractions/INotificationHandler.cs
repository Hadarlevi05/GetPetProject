using GetPet.BusinessLogic.Model;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers.Abstractions
{
    public interface INotificationHandler
    {
        Task UpsertNotificationAsync(int userId, PetFilter filter);

        Task SendNotificationAsync();
    }
}
