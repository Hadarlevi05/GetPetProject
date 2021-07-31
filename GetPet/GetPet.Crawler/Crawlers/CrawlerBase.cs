using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Crawlers.Abstractions;
using GetPet.Crawler.Parsers.Abstractions;
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
            var tasks = animals
                .Where(p => !IsPetExists(p))
                .Select(pet => _petHandler.AddPet(pet));

            await Task.WhenAll(tasks);

            // There is an async/await bug here. Need to investigate. Foir know, lets use the sync version
            // await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.SaveChangesAsync();
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