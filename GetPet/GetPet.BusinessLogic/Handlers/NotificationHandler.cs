using AutoMapper;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers
{
    public class NotificationHandler : INotificationHandler
    {
        protected readonly IPetRepository _petRepository;
        protected readonly INotificationRepository _notificationRepository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMailHandler _mailHandler;
        protected readonly IUserHandler _userHandler;
        protected readonly IUserRepository _userRepository;
        protected readonly IEmailHistoryRepository _emailHistoryRepository;

        public NotificationHandler(
            IPetRepository petRepository,
            INotificationRepository notificationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMailHandler mailHandler,
            IUserHandler userHandler,
            IUserRepository userRepository,
            IEmailHistoryRepository emailHistoryRepository)
        {
            _petRepository = petRepository;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _petRepository = petRepository;
            _notificationRepository = notificationRepository;
            _mailHandler = mailHandler;
            _userHandler = userHandler;
            _userRepository = userRepository;
            _emailHistoryRepository = emailHistoryRepository;
        }


        public async Task UpsertNotificationAsync(int userId, PetFilter filter)
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

        public async Task SendNotificationAsync()
        {
            var userId = 3;
            var user = await _userRepository.GetByIdAsync(userId);
            var notificationFilter = new BusinessLogic.Model.NotificationFilter()
            {
                UserId = userId
            };

            var notifications = await _notificationRepository.SearchAsync(notificationFilter);

            foreach (var notification in notifications)
            {
                var filter = JsonConvert.DeserializeObject<PetFilter>(notification.PetFilterSerialized);
                filter.CreatedSince = DateTime.UtcNow.Date;

                var pets = await _petRepository.SearchAsync(filter);
                if (pets.Any())
                {
                    if (!mailAlreadySent())
                    {
                        var mail = new MailRequest()
                        {
                            ToEmail = user.Email,
                            Subject = "test hadar",
                            Body = "test hadar"

                        };
                        await _mailHandler.SendEmailAsync(mail);
                        var emailHistory = new EmailHistory()
                        {
                            UpdatedTimestamp = DateTime.UtcNow,
                            UserId = userId,
                            NotificationId = notification.Id,
                            CreationTimestamp = DateTime.UtcNow
                        };

                        await _emailHistoryRepository.AddAsync(emailHistory);
                    }

                }

            }
        }

        private bool mailAlreadySent()
        {
            return false;
        }
    }
}