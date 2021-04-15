using System;
using System.Collections.Generic;
using System.Text;
using GetPet.BusinessLogic.Handlers;
using GetPet.Crawler.Parsers;

namespace GetPet.Crawler.Crawlers
{
    public class RehovotSpaCrawler : CrawlerBase<RehovotSpaParser>
    {
        protected override string url => @"http://rehovotspa.org.il/our-dogs/";

        public RehovotSpaCrawler()
        {

        }
    }
}
