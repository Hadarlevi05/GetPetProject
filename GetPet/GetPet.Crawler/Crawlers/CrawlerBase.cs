using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Crawlers.Abstractions;
using GetPet.Crawler.Parsers.Abstractions;
using GetPet.Crawler.Utils;
using GetPet.Data.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GetPet.Crawler.Crawlers
{
    public abstract class CrawlerBase<T> : ICrawler where T : IParser, new()
    {
        protected readonly HtmlDocument doc = new HtmlDocument();
        protected readonly T parser = new T();

        protected readonly IPetHandler _petHandler;
        protected readonly IPetRepository _petRepository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ITraitRepository _traitRepository;
        protected readonly ICityRepository _cityRepository;
        protected readonly IAnimalTypeRepository _animalTypeRepository;
        protected readonly IUserRepository _userRepository;
        protected readonly ITraitOptionRepository _traitOptionRepository;

        private List<Pet> _AllPets;
        private List<User> _AllUsers;

        protected abstract string url { get; }

        public CrawlerBase(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork,
            ITraitRepository traitRepository,
            ICityRepository cityRepository,
            IAnimalTypeRepository animalTypeRepository,
            IUserRepository userRepository,
            ITraitOptionRepository traitOptionRepository
            )
        {
            _petHandler = petHandler;
            _petRepository = petRepository;
            _unitOfWork = unitOfWork;
            _traitRepository = traitRepository;
            _cityRepository = cityRepository;
            _animalTypeRepository = animalTypeRepository;
            _userRepository = userRepository;
            _traitOptionRepository = traitOptionRepository;
        }

        protected virtual async Task<List<Trait>> GetAllTraits()
        {
            var filter = new TraitFilter();
            var results = await _traitRepository.SearchAsync(filter);

            return results.ToList();
        }

        protected virtual async Task<List<City>> GetAllCities()
        {
            var filter = new CityFilter()
            {
                Page = 1,
                PerPage = 1000,
            };

            var results = await _cityRepository.SearchAsync(filter);

            return results.ToList();
        }

        protected virtual async Task<List<AnimalType>> GetAllAnimalTypes()
        {
            var filter = new BaseFilter();
            var results = await _animalTypeRepository.SearchAsync(filter);

            return results.ToList();
        }

        public virtual async Task Load(string url)
        {
            string html = await new WebClient().DownloadStringTaskAsync(new Uri(url));

            doc.LoadHtml(html);

            parser.Document = doc;

            _AllPets = _petRepository.SearchAsync(new PetFilter() { Page = 1, PerPage = 1000 }).Result.ToList();
            _AllUsers = _userRepository.SearchAsync(new UserFilter() { Page = 1, PerPage = 1000 }).Result.ToList();
        }

        public virtual async Task Load()
        {
            await Load(url);
        }

        public virtual async Task<IList<Pet>> Parse()
        {
            var traits = await GetAllTraits();
            var animalTypes = await GetAllAnimalTypes();

            var user = await CreateUser();

            return parser.Parse(traits, user, animalTypes);
        }

        public virtual async Task InsertToDB(IList<Pet> animals)
        {
            foreach (var animal in animals)
            {
                if (animal.PetTraits == null)
                {
                    animal.PetTraits = new List<PetTrait>();
                }
                await AddAgeTrait(animal);
                await AddGenderTrait(animal);

            }
            var tasks = animals
                .Where(p => !IsPetExists(p))
                .Select(pet => _petHandler.AddPet(pet));

            await Task.WhenAll(tasks);

            // There is an async/await bug here. Need to investigate. Foir know, lets use the sync version
            // await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task AddAgeTrait(Pet animal)
        {
            var traitAge = await _traitRepository.SearchAsync(new TraitFilter()
            {
                PerPage = 1000,
                TraitName = "גיל"
            });

            var age = new Age(animal.Birthday);
            var ageInMonth = age.Years * 12 + age.Months;

            // גור עד 9 חודשים
            // צעיר - 9-24 חודשים
            // בוגר 2-7 שנים
            // מבוגר - מעל 7 שנים
            string option = string.Empty;
            if (ageInMonth <= 9)
            {
                option = "גור";
            }
            else if (ageInMonth > 9 && ageInMonth <= 24)
            {
                option = "צעיר";
            }
            else if (age.Years > 2 && age.Years <= 7)
            {
                option = "בוגר";
            }
            else
            {
                option = "מבוגר";
            }
            var traitAgeId = traitAge.Where(t=> t.AnimalTypeId == animal.AnimalTypeId).FirstOrDefault().Id;
            var options = await _traitOptionRepository.SearchAsync(new TraitOptionFilter()
            {
                TraitId = traitAgeId
            });

            var optionId = options.Where(o => o.Option == option).FirstOrDefault().Id;
            animal.PetTraits.Add(new PetTrait()
            {
                TraitId = traitAgeId,
                TraitOptionId = optionId
            });
        }

        private async Task AddGenderTrait(Pet animal)
        {
            var traitGenderID = await _traitRepository.SearchAsync(new TraitFilter()
            {
                PerPage = 1000,
                TraitName = "מין"
            });

            var id = traitGenderID.Where(t => t.AnimalTypeId == animal.AnimalTypeId).FirstOrDefault().Id;
            var options = await _traitOptionRepository.SearchAsync(new TraitOptionFilter()
            {
                TraitId = id
            });
            string gender = (int)animal.Gender == 1 ? "זכר" : "נקבה";
            var optionId = options.Where(o => o.Option == gender).FirstOrDefault().Id;
            animal.PetTraits.Add(new PetTrait()
            {
                TraitId = id,
                TraitOptionId = optionId
            });
        }

        protected bool IsPetExists(Pet pet)
        {
            return _AllPets.Any(p => p.Name.Equals(pet.Name) && p.SourceLink == pet.SourceLink);
        }

        protected User IsUserExists(string name, string phoneNember)
        {
            return _AllUsers.FirstOrDefault(u => u.Name.Equals(name) && u.PhoneNumber.Equals(phoneNember));
        }

        public abstract Task<User> CreateUser();
    }
}