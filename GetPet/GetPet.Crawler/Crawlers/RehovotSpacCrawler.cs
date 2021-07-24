using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Parsers;
using GetPet.Data.Entities;
using PetAdoption.BusinessLogic.Repositories;
using System;

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

        public override User CreateUser()
        {
            return new User()
            {
                Name = "RehovotSpca",
                Email = "RehovotSpca@gmail.com",
                UserType = Data.Enums.UserType.Organization,
                Organization = new Organization()
                {
                    Name = "RehovotSpca",
                    Email = "RehovotSpca@gmail.com",
                },
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                PasswordHash = "1234",
                CityId = 1,
            };
        }
    }
}