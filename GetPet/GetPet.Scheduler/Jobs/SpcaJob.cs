using GetPet.Crawler.Crawlers;
using GetPet.Scheduler.Jobs.Abstraction;
using System;
using System.Threading.Tasks;

namespace GetPet.Scheduler.Jobs
{
    public class SpcaJob : IJob
    {
        private readonly SpcaCrawler _spcaCrawler;

        public SpcaJob(SpcaCrawler spcaCrawler)
        {
            _spcaCrawler = spcaCrawler;            
        }

        public async Task Execute()
        {
            Console.WriteLine($"{nameof(SpcaJob)} Job starting run");

            await _spcaCrawler.Load();

            var result = await _spcaCrawler.Parse();

            await _spcaCrawler.InsertPets(result);

            Console.WriteLine($"{nameof(SpcaJob)} Job Ending run");
        }
    }
}