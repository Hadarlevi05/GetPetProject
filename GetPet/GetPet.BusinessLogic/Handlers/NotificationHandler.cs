using AutoMapper;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using PetAdoption.BusinessLogic.Repositories;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace GetPet.BusinessLogic.Handlers
{
    public class NotificationHandler : INotificationHandler
    {
        protected readonly IPetRepository _petRepository;
        protected readonly INotificationRepository _notificationRepository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public NotificationHandler(
            IPetRepository petRepository,
            INotificationRepository notificationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task UpsertNotification(int userId, PetFilter filter)
        {
            var notifications = await _notificationRepository.SearchAsync(new NotificationFilter
            {
                UserId = userId
            });

            var petFilters = notifications.Select(n => JsonConvert.DeserializeObject<PetFilter>(n.PetFilterSerialized));

            if (petFilters.Any(pf => pf.GetHashCode() == filter.GetHashCode()))
                return;

            await _notificationRepository.AddAsync(new Data.Entities.Notification
            {
                UserId = userId,
                PetFilterSerialized = JsonConvert.SerializeObject(filter)

            });

            await _unitOfWork.SaveChangesAsync();
        }

    }
}