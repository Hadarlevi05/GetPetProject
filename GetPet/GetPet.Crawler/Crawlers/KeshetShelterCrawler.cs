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
    public class KeshetShelterCrawler: CrawlerBase<KeshetShelterParser>
    {
        protected override string url => @"https://keshet-shelter.co.il/statuses/%d7%9c%d7%90%d7%99%d7%9e%d7%95%d7%a5/";

        public KeshetShelterCrawler(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork,
            ITraitRepository traitRepository,
            ICityRepository cityRepository,
            IAnimalTypeRepository animalTypeRepository,
            IUserRepository userRepository
            ) :
            base(petHandler, petRepository, unitOfWork, traitRepository, cityRepository, animalTypeRepository, userRepository)
        { }

        public override async Task<User> CreateUser()
        {
            var allCities = await GetAllCities();
            var city = allCities.FirstOrDefault(x => x.Name == "קריית ביאליק");

            User user = new User()
            {
                Name = "בית מחסה קשת",
                Email = "",
                UserType = Data.Enums.UserType.Organization,
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                PasswordHash = "1234",
                CityId = city.Id,
                PhoneNumber = "054-9207733",
                Organization = new Organization()
                {
                    Name = "בית מחסה קשת",
                    Email = "",
                    PhoneNumber = "054-9207733",
                },
            };

            var existingUser = IsUserExists(user.Name, user.PhoneNumber);

            return (existingUser != null) ? existingUser : user;
        }

    }
}
