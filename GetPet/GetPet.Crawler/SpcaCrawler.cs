using GetPet.BusinessLogic.Handlers;
using GetPet.BusinessLogic.Model;
using GetPet.Crawler.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.Crawler
{
    public class SpcaCrawler : CrawlerBase<SpcaParser>
    {
        protected override string url => @"https://spca.co.il/%D7%90%D7%99%D7%9E%D7%95%D7%A6%D7%99%D7%9D/";

        public SpcaCrawler()
        {
        }
    }
}
