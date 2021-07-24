using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Parsers;
using GetPet.Data.Entities;
using PetAdoption.BusinessLogic.Repositories;
using System;
using System.Linq;

namespace GetPet.Crawler.Crawlers
{
    public class RehovotSpaCrawler : CrawlerBase<RehovotSpaParser>
    {
        protected override string url => @"http://rehovotspa.org.il/our-dogs/";

        public RehovotSpaCrawler(
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

        public override User CreateUser()
        {
            var allCities = GetAllCities();
            var city = allCities.FirstOrDefault(x => x.Name == "רחובות");

            User user = new User()
            {
                Name = "צער בעלי חיים רחובות",
                Email = "info@rehovotSpca.co.il",
                UserType = Data.Enums.UserType.Organization,
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                PasswordHash = "1234",
                CityId = city.Id,
                PhoneNumber = "08-9460135",
                Organization = new Organization()
                {
                    Name = "צער בעלי חיים רחובות",
                    Email = "info@rehovotSpca.co.il",
                    PhoneNumber = "08-9460135",
                },
            };

            var existingUser = IsUserExists(user.Name, user.PhoneNumber);

            return (existingUser != null) ? existingUser : user;
        }
    }
}