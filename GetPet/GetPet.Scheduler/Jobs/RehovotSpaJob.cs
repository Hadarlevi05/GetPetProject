using GetPet.Crawler.Crawlers;
using GetPet.Scheduler.Jobs.Abstraction;
using System;
using System.Threading.Tasks;

namespace GetPet.Scheduler.Jobs
{
    public class RehovotSpaJob : IJob
    {
        private readonly RehovotSpaCrawler _rehovotSpaCrawler;

        public RehovotSpaJob(RehovotSpaCrawler rehovotSpaCrawler)
        {
            _rehovotSpaCrawler = rehovotSpaCrawler;            
        }

        public async Task Execute()
        {
            Console.WriteLine($"{nameof(RehovotSpaJob)} Job starting run");

            await _rehovotSpaCrawler.Load();

            var result = await _rehovotSpaCrawler.Parse();

            await _rehovotSpaCrawler.InsertToDB(result);

            Console.WriteLine($"{nameof(RehovotSpaJob)} Job Ending run");
        }
    }
}