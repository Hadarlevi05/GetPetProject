using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.Crawler.Parsers;
using PetAdoption.BusinessLogic.Repositories;

namespace GetPet.Crawler.Crawlers
{
    public class SpcaCrawler : CrawlerBase<SpcaParser>
    {
        protected override string url => @"https://spca.co.il/%D7%90%D7%99%D7%9E%D7%95%D7%A6%D7%99%D7%9D/";

        public SpcaCrawler(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork) :
            base(petHandler, petRepository, unitOfWork)
        { }
    }
}