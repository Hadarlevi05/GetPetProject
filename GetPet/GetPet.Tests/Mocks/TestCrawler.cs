using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Crawlers;
using GetPet.Crawler.Parsers.Abstractions;
using PetAdoption.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GetPet.Tests.Mocks
{
    public class TestCrawler<T> : CrawlerBase<T> where T : IParser, new()
    {
        public TestCrawler(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork,
            ITraitRepository traitRepository) :
            base(petHandler, petRepository, unitOfWork, traitRepository)
        {
        }

        protected override string url => throw new NotImplementedException();

        public override void Load(string url)
        {
            if (!File.Exists(url))
            {
                throw new Exception("Cannot find file");
            }

            using (var file = File.OpenRead(url))
            {
                doc.Load(file);
                parser.Document = doc;
            }
        }
    }
}
