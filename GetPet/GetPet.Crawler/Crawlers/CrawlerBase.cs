using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Crawlers.Abstractions;
using GetPet.Crawler.Parsers.Abstractions;
using GetPet.Data.Entities;
using HtmlAgilityPack;
using PetAdoption.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GetPet.Crawler.Crawlers
{
    public abstract class CrawlerBase<T> : ICrawler where T : IParser, new()
    {
        protected readonly HtmlDocument doc = new HtmlDocument();
        protected static readonly WebClient client = new WebClient();
        protected readonly T parser = new T();

        protected readonly IPetHandler _petHandler;
        protected readonly IPetRepository _petRepository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ITraitRepository _traitRepository;
        protected readonly ICityRepository _cityRepository;
        protected readonly IAnimalTypeRepository _animalTypeRepository;
        protected readonly IUserRepository _userRepository;

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
            IUserRepository userRepository
            )
        {
            _petHandler = petHandler;
            _petRepository = petRepository;
            _unitOfWork = unitOfWork;
            _traitRepository = traitRepository;
            _cityRepository = cityRepository;
            _animalTypeRepository = animalTypeRepository;
            _userRepository = userRepository;
        }

        protected virtual List<Trait> GetAllTraits()
        {
            var filter = new TraitFilter();
            var results = _traitRepository.SearchAsync(filter).Result.ToList();

            return results;
        }

        protected virtual List<City> GetAllCities()
        {
            var filter = new CityFilter()
            {
                Page = 1,
                PerPage = 1000,
            };

            var results = _cityRepository.SearchAsync(filter).Result.ToList();

            return results;
        }

        protected virtual List<AnimalType> GetAllAnimalTypes()
        {
            var filter = new BaseFilter();
            var results = _animalTypeRepository.SearchAsync(filter).Result.ToList();

            return results;
        }

        public virtual void Load(string url)
        {
            string html = client.DownloadString(url);

            doc.LoadHtml(html);

            parser.Document = doc;

            _AllPets = _petRepository.SearchAsync(new PetFilter() { Page = 1, PerPage = 1000 }).Result.ToList();
            _AllUsers = _userRepository.SearchAsync(new UserFilter() { Page = 1, PerPage = 1000 }).Result.ToList();
        }

        public virtual void Load()
        {
            Load(url);
        }

        public virtual IList<Pet> Parse()
        {
            var traits = GetAllTraits();
            var animalTypes = GetAllAnimalTypes();

            var user = CreateUser();

            return parser.Parse(traits, user, animalTypes);
        }

        public virtual async void InsertToDB(IList<Pet> animals)
        {
            var tasks = animals
                .Where(p => !IsPetExists(p))
                .Select(pet => _petHandler.AddPet(pet));

            await Task.WhenAll(tasks);

            // There is an async/await bug here. Need to investigate. Foir know, lets use the sync version
            // await _unitOfWork.SaveChangesAsync();
            _unitOfWork.SaveChanges();
        }

        protected bool IsPetExists(Pet pet)
        {
            return _AllPets.Any(p => p.Name.Equals(pet.Name) && p.SourceLink == pet.SourceLink); 
        }

        protected User IsUserExists(string name, string phoneNember)
        {
            return _AllUsers.FirstOrDefault(u => u.Name.Equals(name) && u.PhoneNumber.Equals(phoneNember));
        }

        public abstract User CreateUser();
    }
}