using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Parsers.Abstractions;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net;

namespace GetPet.Crawler
{
    public abstract class CrawlerBase<T> where T : IParser, new()
    {
        private readonly HtmlDocument doc = new HtmlDocument();
        private readonly WebClient client = new WebClient();
        private readonly T parser = new T();

        public CrawlerBase() { }

        public void Load(string url)
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