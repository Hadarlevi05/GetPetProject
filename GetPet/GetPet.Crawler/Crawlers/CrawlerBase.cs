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
        protected readonly WebClient client = new WebClient();
        protected readonly T parser = new T();

        protected readonly IPetHandler _petHandler;
        protected readonly IPetRepository _petRepository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ITraitRepository _traitRepository;

        private List<Pet> pets;
        protected abstract string url { get; }

        public CrawlerBase(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork,
            ITraitRepository traitRepository)
        {
            _petHandler = petHandler;
            _petRepository = petRepository;
            _unitOfWork = unitOfWork;
            _traitRepository = traitRepository;
        }

        public virtual void Load(string url)
        {
            string html = client.DownloadString(url);

            doc.LoadHtml(html);

            parser.Document = doc;

            pets = _petRepository.SearchAsync(new PetFilter() { Page = 1, PerPage = 1000 }).Result.ToList();
        }

        public virtual void Load()
        {
            Load(url);
        }

        public virtual IList<PetDto> Parse()
        {
            return parser.Parse();
        }

        public virtual async void InsertToDB(IList<PetDto> animals)
        {            
            var tasks = animals
                .Where(p => !IsPetExists(p))
                .Select(pet => _petHandler.AddPet(pet));

            await Task.WhenAll(tasks);

            await _unitOfWork.SaveChangesAsync();
        }

        private bool IsPetExists(PetDto pet)
        {
            // TODO: Add source (which website) to animals

            return pets.Any(p => p.Name == pet.Name); // TODO: Better comapring conditions. Consider using 'Equals'
        }
    }
}