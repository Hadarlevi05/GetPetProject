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
    public class RlaCrawler : CrawlerBase<RlaParser>
    {
        protected override string url => @"https://www.rla.org.il/adoption/dogs/";  //dogs
        protected override string url2 => @"https://www.rla.org.il/adoption/cats/"; //cats

        public RlaCrawler(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork,
            ITraitRepository traitRepository,
            ICityRepository cityRepository,
            IAnimalTypeRepository animalTypeRepository,
            IUserRepository userRepository,
            ITraitOptionRepository traitOptionRepository,
            RlaParser parser) :
            base(petHandler, petRepository, unitOfWork, traitRepository, cityRepository, animalTypeRepository, userRepository, traitOptionRepository, parser)
        { }

        public override async Task<User> CreateUser()
        {
            var allCities = await GetAllCities();
            var city = allCities.FirstOrDefault(x => x.Name == "ראשון לציון");

            User user = new User()
            {
                Name = "ראשון אוהבת חיות",
                Email = "rla.org.il@gmail.com",
                UserType = Data.Enums.UserType.Organization,
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                PasswordHash = "1234",
                CityId = city.Id,
                PhoneNumber = "052-3264943",
                Organization = new Organization()
                {
                    Name = "ראשון אוהבת חיות",
                    Email = "rla.org.il@gmail.com",
                    PhoneNumber = "052-3264943",
                },
            };
            var existingUser = await IsUserExists(user) ?? user;

            return existingUser; 
        }
    }
}
