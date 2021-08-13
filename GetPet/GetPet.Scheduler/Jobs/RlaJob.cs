using GetPet.Crawler.Crawlers;
using GetPet.Scheduler.Jobs.Abstraction;
using System;
using System.Threading.Tasks;

namespace GetPet.Scheduler.Jobs
{
    public class RlaJob : IJob
    {
        private readonly RlaCrawler _rlaCrawler;

        public RlaJob(RlaCrawler rlaCrawler)
        {
            _rlaCrawler = rlaCrawler;
        }

        public async Task Execute()
        {
            Console.WriteLine($"{nameof(RehovotSpaJob)} Job starting run");

            await _rlaCrawler.Load();

            var result = await _rlaCrawler.Parse();

            await _rlaCrawler.InsertPets(result);

            Console.WriteLine($"{nameof(RehovotSpaJob)} Job Ending run");
        }
    }
}