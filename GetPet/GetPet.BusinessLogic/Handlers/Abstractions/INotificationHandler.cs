using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Model.Filters;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers.Abstractions
{
    public interface INotificationHandler
    {
        Task UpsertNotificationAsync(int userId, PetFilter filter);

        Task SendNotificationAsync();
    }
}
