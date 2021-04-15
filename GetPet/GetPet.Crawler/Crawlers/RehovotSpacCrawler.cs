using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.Crawler.Parsers;
using PetAdoption.BusinessLogic.Repositories;

namespace GetPet.Crawler.Crawlers
{
    public class RehovotSpaCrawler : CrawlerBase<RehovotSpaParser>
    {
        protected override string url => @"http://rehovotspa.org.il/our-dogs/";

        public RehovotSpaCrawler(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork) :
            base(petHandler, petRepository, unitOfWork)
        { }
    }
}