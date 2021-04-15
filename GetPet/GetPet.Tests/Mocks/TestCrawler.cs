using GetPet.Crawler;
using GetPet.Crawler.Crawlers;
using GetPet.Crawler.Parsers;
using GetPet.Crawler.Parsers.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GetPet.Tests.Mocks
{
    //public class TestCrawler<T> : CrawlerBase<T> where T : IParser, new()
    //{
    //    protected override string url => throw new NotImplementedException();

    //    public override void Load(string url)
    //    {
    //        if (!File.Exists(url))
    //        {
    //            throw new Exception("Cannot find file");
    //        }

    //        using (var file = File.OpenRead(url))
    //        {
    //            doc.Load(file);
    //            parser.Document = doc;
    //        }
    //    }
    //}
}
