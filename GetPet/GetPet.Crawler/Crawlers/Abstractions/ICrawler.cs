using GetPet.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetPet.Crawler.Crawlers.Abstractions
{
    public interface ICrawler
    {
        Task Load(string url);

        Task Load();

        Task<IList<Pet>> Parse();

        Task InsertToDB(IList<Pet> input);

        Task<User> CreateUser();
    }
}
