using GetPet.Crawler.Crawlers;
using GetPet.Scheduler.Jobs.Abstraction;
using System;
using System.Threading.Tasks;

namespace GetPet.Scheduler.Jobs
{
    public class KeshetShelterJob : IJob
    {
        private readonly KeshetShelterCrawler _keshetShelterCrawler;

        public KeshetShelterJob (KeshetShelterCrawler keshetShelterCrawler)
        {
            _keshetShelterCrawler = keshetShelterCrawler;
        }

        public async Task Execute()
        {
            Console.WriteLine($"{nameof(KeshetShelterJob)} Job starting run");

            await _keshetShelterCrawler.Load();

            var result = await _keshetShelterCrawler.Parse();

            await _keshetShelterCrawler.InsertToDB(result);

            Console.WriteLine($"{nameof(KeshetShelterJob)} Job Ending run");
        }

    }
}
