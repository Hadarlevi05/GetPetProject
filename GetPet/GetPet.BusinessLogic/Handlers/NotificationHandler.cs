using AutoMapper;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Model.Filters;
using GetPet.BusinessLogic.Repositories;
using GetPet.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
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
        protected readonly MailSettings _mailSettings;

        public NotificationHandler(
            IPetRepository petRepository,
            INotificationRepository notificationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMailHandler mailHandler,
            IUserHandler userHandler,
            IUserRepository userRepository,
            IEmailHistoryRepository emailHistoryRepository,
            MailSettings mailSettings)
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
            _mailSettings = mailSettings;
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
            var notifications = await _notificationRepository.SearchAsync(new NotificationFilter());
            var users = (await _userRepository.GetByIdAsync(notifications.Select(n => n.UserId))).ToDictionary(i => i.Id, i => i);

            foreach (var notification in notifications)
            {
                var filter = JsonConvert.DeserializeObject<PetFilter>(notification.PetFilterSerialized);
                filter.CreatedSince = DateTime.Now.Date;

                var user = users[notification.UserId];

                var pets = await _petRepository.SearchAsync(filter);
                var attachment = new List<Attachment>();
                if (mailAlreadySent())
                {
                    continue;
                }

                string mailTemplate = GetResource("notification-email.html")
                    .Replace("{{user.Name}}", user.Name);
                string petRowTemplate = GetResource("notification-pet-row.html");

                StringBuilder sbPets = new StringBuilder();

                int fileCount = 0;

                foreach (var pet in pets)
                {
                    string html = petRowTemplate
                        .Replace("{{pet.Name}}", pet.Name)
                        .Replace("{{pet.Description}}", pet.Description)
                        .Replace("{{pet.SourceLink}}", pet.SourceLink);

                    using (var client = new WebClient())
                    {
                        var content = client.DownloadData(pet.MetaFileLinks.First().Path);
                        using (var ms = new MemoryStream(content))
                        {
                            string imageName = $"image{fileCount++}.jpg";

                            var fileBytes = ms.ToArray();
                            attachment.Add(new Attachment(new MemoryStream(fileBytes), imageName));

                            html = html.Replace("{{pet-image}}", $"cid:{imageName}");
                        }
                    }
                    sbPets.AppendLine(html);
                }

                var bodyHtml = mailTemplate.Replace("{{pets-html}}", sbPets.ToString());

                var mail = new MailRequest()
                {
                    To = user.Email,
                    Subject = "בעלי חיים חדשים מחכים לאימוץ!",
                    Body = bodyHtml,
                    Attachments = attachment
                };
                await _mailHandler.SendEmailAsync(mail);

                var emailHistory = new EmailHistory()
                {
                    UpdatedTimestamp = DateTime.Now,
                    UserId = user.Id,
                    NotificationId = notification.Id,
                    CreationTimestamp = DateTime.Now
                };
                await _emailHistoryRepository.AddAsync(emailHistory);
                await _unitOfWork.SaveChangesAsync();

            }
        }

        private bool mailAlreadySent()
        {
            return false;
        }

        private string GetResource(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"GetPet.BusinessLogic.Resources.{filename}";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
}