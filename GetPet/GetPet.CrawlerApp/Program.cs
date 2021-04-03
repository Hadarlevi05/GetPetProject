using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.MappingProfiles;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler;
using GetPet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetAdoption.BusinessLogic.Repositories;

namespace GetPet.CrawlerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Fix app settings
            //string sqlConnectionString = Configuration.GetConnectionString("GetPetConnectionString");
            string sqlConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=GetPet;Integrated Security=True";

            IServiceCollection services = new ServiceCollection();

            services.AddAutoMapper(typeof(GetPetProfile));

            var serviceProvider = services
                .AddDbContext<GetPetDbContext>(options => options.UseSqlServer(sqlConnectionString))
                .AddScoped<IPetRepository, PetRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IGetPetDbContextSeed, GetPetDbContextSeed>()
                .AddScoped<IPetHandler, PetHandler>()
                .AddScoped<RehovotSpaCrawler>()
                .AddScoped<IUnitOfWork, UnitOfWork>().BuildServiceProvider();

            // TODO: Need to decide if each crawler (scraper?) runs on different thread or not
            LaunchCrawlerForExample(serviceProvider);
        }

        private static void LaunchCrawlerForExample(ServiceProvider serviceProvider)
        {
            var crawler = serviceProvider.GetService<RehovotSpaCrawler>();
            var db = serviceProvider.GetService<IPetHandler>();

            crawler.Load();
            var result = crawler.Parse();
            crawler.InsertToDB(db, result);
        }
    }
}
