using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.Scheduler.Jobs.Abstraction;
using System;
using System.Threading.Tasks;

namespace GetPet.Scheduler.Jobs
{
    public class NotificationSenderJob : IJob
    {
        private readonly INotificationHandler _notificationHandler;

        public NotificationSenderJob(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        public async Task Execute()
        {
            Console.WriteLine($"{nameof(NotificationSenderJob)} Job starting run");

            await _notificationHandler.SendNotificationAsync();

            Console.WriteLine($"{nameof(NotificationSenderJob)} Job ending run");
        }
    }
}