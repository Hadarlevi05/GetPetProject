using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Repositories;
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
            IUnitOfWork unitOfWork,
            ITraitRepository traitRepository) :
            base(petHandler, petRepository, unitOfWork, traitRepository)
        { }
    }
}