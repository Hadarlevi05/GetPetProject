using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Parsers.Abstractions;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net;

namespace GetPet.Crawler
{
    public abstract class CrawlerBase<T> where T : IParser, new()
    {
        protected readonly HtmlDocument doc = new HtmlDocument();
        protected readonly WebClient client = new WebClient();
        protected readonly T parser = new T();
        protected virtual string url { get; }

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

        public virtual void InsertToDB(IPetHandler db, IList<PetDto> input)
        {
            foreach (var pet in input)
            {
                db.AddPet(pet);
            }
        }
    }
}