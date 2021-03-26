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

        public CrawlerBase() { }

        public virtual void Load(string url)
        {
            string html = client.DownloadString(url);

            doc.LoadHtml(html);

            parser.Document = doc;
        }

        public virtual IList<PetDto> Parse()
        {
            return parser.Parse();
        }
    }
}