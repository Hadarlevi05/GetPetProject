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
    public class JspcaCrawler : CrawlerBase<JspcaParser>
    {
        protected override string url => @"https://jspca.org.il/pets/?pet_type=dog";  //dogs
        protected override string url2 => @"https://jspca.org.il/pets/?pet_type=cat"; //cats

        public JspcaCrawler(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork,
            ITraitRepository traitRepository,
            ICityRepository cityRepository,
            IAnimalTypeRepository animalTypeRepository,
            IUserRepository userRepository,
            ITraitOptionRepository traitOptionRepository,
            JspcaParser parser) :
            base(petHandler, petRepository, unitOfWork, traitRepository, cityRepository, animalTypeRepository, userRepository, traitOptionRepository, parser)
        { }

        public override async Task<User> CreateUser()
        {
            var allCities = await GetAllCities();
            var city = allCities.FirstOrDefault(x => x.Name == "ירושלים");

            User user = new User()
            {
                Name = "האגודה לצער בעלי-חיים ירושלים",
                Email = "jspca.jeru@gmail.com",
                UserType = Data.Enums.UserType.Organization,
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                PasswordHash = "1234",
                CityId = city.Id,
                PhoneNumber = "02-585-4465",
                Organization = new Organization()
                {
                    Name = "האגודה לצער בעלי-חיים ירושלים",
                    Email = "jspca.jeru@gmail.com",
                    PhoneNumber = "02-585-4465",
                },
            };
            var existingUser = await IsUserExists(user) ?? user;

            return existingUser;
        }
    }
}
