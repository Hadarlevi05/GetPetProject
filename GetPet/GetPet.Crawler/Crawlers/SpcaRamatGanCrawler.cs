using System;
using System.Collections.Generic;
using System.Text;
using GetPet.BusinessLogic.Handlers;
using GetPet.Crawler.Parsers;

namespace GetPet.Crawler.Crawlers
{
    public class SpcaRamatGanCrawler : CrawlerBase<SpcaRamatGanParser>
    {
        protected override string url => throw new NotImplementedException();

        public SpcaRamatGanCrawler()
        {

        }
    }
}