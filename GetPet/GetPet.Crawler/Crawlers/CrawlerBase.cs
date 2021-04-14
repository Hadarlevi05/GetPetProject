using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Crawlers.Abstractions;
using GetPet.Crawler.Parsers.Abstractions;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace GetPet.Crawler.Crawlers
{
    public abstract class CrawlerBase<T> : ICrawler where T : IParser, new()
    {
        protected readonly HtmlDocument doc = new HtmlDocument();
        protected readonly WebClient client = new WebClient();
        protected readonly T parser = new T();
        protected abstract string url { get; }

        public CrawlerBase() 
        {
        }

        public virtual void Load(string url)
        {
            string html = client.DownloadString(url);

            doc.LoadHtml(html);

            parser.Document = doc;
        }

        public virtual void Load()
        {
            Load(url);
        }

        public virtual IList<PetDto> Parse()
        {
            return parser.Parse();
        }

        public virtual async void InsertToDB(IPetHandler db, IList<PetDto> input)
        {
            var animals = input.Where(p => !IsPetExists(db, p));

            foreach (var pet in animals)
            {
                await db.AddPet(pet);
            }
        }

        private bool IsPetExists(IPetHandler db, PetDto pet)
        {
            var allAnimals = new List<PetDto>(); // TODO: Get all animals from DB

            // TODO: Add source (which website) to animals

            return (allAnimals.Any(p => p.Name == pet.Name)); // TODO: Better comapring conditions. Consider using 'Equals'
        }
    }
}