using GetPet.Scheduler.Jobs.Abstraction;
using System;
using System.Threading.Tasks;

namespace GetPet.Scheduler.Jobs
{
    public class NotificationSenderJob : IJob
    {
        public Task Execute()
        {
            Console.WriteLine($"{nameof(NotificationSenderJob)} Job starting run");

            return null;
        }
    }
}