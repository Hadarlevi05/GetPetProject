using GetPet.Crawler.Crawlers;
using GetPet.Scheduler.Jobs.Abstraction;
using System;
using System.Threading.Tasks;

namespace GetPet.Scheduler.Jobs
{
    public class JspcaJob : IJob
    {
        private readonly JspcaCrawler _jspcaCrawler;

        public JspcaJob(JspcaCrawler jspcaCrawler)
        {
            _jspcaCrawler = jspcaCrawler;
        }

        public async Task Execute()
        {
            Console.WriteLine($"{nameof(JspcaJob)} Job starting run");

            await _jspcaCrawler.Load();

            var result = await _jspcaCrawler.Parse();

            await _jspcaCrawler.InsertPets(result);

            Console.WriteLine($"{nameof(JspcaJob)} Job Ending run");
        }
    }
}