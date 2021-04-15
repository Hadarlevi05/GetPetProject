using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.Crawler.Parsers;
using PetAdoption.BusinessLogic.Repositories;
using System;

namespace GetPet.Crawler.Crawlers
{
    public class SpcaRamatGanCrawler : CrawlerBase<SpcaRamatGanParser>
    {
        protected override string url => throw new NotImplementedException();

        public SpcaRamatGanCrawler(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork) :
            base(petHandler, petRepository, unitOfWork)
        { }        
    }
}