using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Parsers;
using GetPet.Data.Entities;
using PetAdoption.BusinessLogic.Repositories;
using System;

namespace GetPet.Crawler.Crawlers
{
    public class SpcaCrawler : CrawlerBase<SpcaParser>
    {
        protected override string url => @"https://spca.co.il/%D7%90%D7%99%D7%9E%D7%95%D7%A6%D7%99%D7%9D/";

        public SpcaCrawler(
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
                Name = "Spca",
                Email = "Spca@gmail.com",
                UserType = Data.Enums.UserType.Organization,
                Organization = new Organization()
                {
                    Name = "Spca",
                    Email = "Spca@gmail.com",
                },
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                PasswordHash = "1234",
                CityId = 1,
            };
        }
    }
}