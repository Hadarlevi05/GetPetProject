using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Parsers;
using GetPet.Data.Entities;
using GetPet.BusinessLogic.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.Crawler.Crawlers
{
    public class SpcaCrawler : CrawlerBase<SpcaParser>
    {
        protected override string url => @"https://spca.co.il/%D7%90%D7%99%D7%9E%D7%95%D7%A6%D7%99%D7%9D/";
        protected override string url2 => null;


        public SpcaCrawler(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork,
            ITraitRepository traitRepository,
            ICityRepository cityRepository,
            IAnimalTypeRepository animalTypeRepository,
            IUserRepository userRepository,
            ITraitOptionRepository traitOptionRepository
            ) :
            base(petHandler, petRepository, unitOfWork, traitRepository, cityRepository, animalTypeRepository, userRepository, traitOptionRepository)
        { }

        public override async Task<User> CreateUser()
        {
            var allCities = await GetAllCities();
            var city = allCities.FirstOrDefault(x => x.Name == "רמת גן");

            var user = new User()
            {
                Name = "צער בעלי חיים ישראל",
                Email = "info@spca.co.il",
                UserType = Data.Enums.UserType.Organization,
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                PasswordHash = "1234",
                CityId = city.Id,
                PhoneNumber = "03-5136500",
                Organization = new Organization()
                {
                    Name = "צער בעלי חיים ישראל",
                    Email = "info@spca.co.il",
                    PhoneNumber = "03-5136500",
                },
            };

            var existingUser = await IsUserExists(user) ?? user;

            return existingUser;
        }
    }
}