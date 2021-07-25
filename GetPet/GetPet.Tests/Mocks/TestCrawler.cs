using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Crawlers;
using GetPet.Crawler.Parsers.Abstractions;
using GetPet.Data.Entities;
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
            ITraitRepository traitRepository,
            ICityRepository cityRepository,
            IAnimalTypeRepository animalTypeRepository,
            IUserRepository userRepository
            ) :
            base(petHandler, petRepository, unitOfWork, traitRepository, cityRepository, animalTypeRepository, userRepository)
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

        public override User CreateUser()
        {
            return new User()
            {
                Name = "Testing",
                Email = "Testing@gmail.com",
                UserType = Data.Enums.UserType.Organization,
                Organization = new Organization()
                {
                    Name = "Testing",
                    Email = "Testing@gmail.com",
                },
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                PasswordHash = "1234",
                CityId = 1,
            };
        }
    }
}
