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
                Name = "צער בעלי חיים ישראל",
                Email = "info@spca.co.il",
                UserType = Data.Enums.UserType.Organization,
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                PasswordHash = "1234",
                CityId = 1,
                PhoneNumber = "03-5136500",
                Organization = new Organization()
                {
                    Name = "צער בעלי חיים ישראל",
                    Email = "info@spca.co.il",
                    PhoneNumber = "03-5136500",
                },
            };
        }
    }
}