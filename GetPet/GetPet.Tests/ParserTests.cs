using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.MappingProfiles;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Crawlers;
using GetPet.Crawler.Crawlers.Abstractions;
using GetPet.Crawler.Parsers;
using GetPet.Data;
using GetPet.Data.Enums;
using GetPet.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using GetPet.BusinessLogic.Repositories;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GetPet.BusinessLogic.Azure;

namespace GetPet.Tests
{
    public class Tests
    {
        private static IPetHandler petHandler;
        private static IPetRepository petRepository;
        private static IUnitOfWork unitOfWork;
        private static ITraitRepository traitRepository;
        private static ICityRepository cityRepository;
        private static IAnimalTypeRepository animalTypeRepository;
        private static IUserRepository userRepository;
        private static ITraitOptionRepository traitOptionRepository;
        private static AzureBlobHelper _azureBlobHelper;


        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            petRepository = serviceProvider.GetService<IPetRepository>();
            petHandler = serviceProvider.GetService<IPetHandler>();
            unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            traitRepository = serviceProvider.GetService<ITraitRepository>();
            cityRepository = serviceProvider.GetService<ICityRepository>();
            animalTypeRepository = serviceProvider.GetService<IAnimalTypeRepository>();
            userRepository = serviceProvider.GetService<IUserRepository>();

            _azureBlobHelper = new AzureBlobHelper(new Common.ImageHelper());

        }

        [Test]
        public async Task MockTest()
        {
            // ctrl r+t
            var crawler = new TestCrawler<SpcaParser>(petHandler, petRepository, unitOfWork, traitRepository, cityRepository, animalTypeRepository, userRepository, traitOptionRepository, new SpcaParser(_azureBlobHelper));
            string file = Path.Combine(Environment.CurrentDirectory, "Files\\Spca.html");

            await crawler.Load(file,null);

            var pets = await crawler.Parse();

            Assert.AreEqual(pets.Count, 22);

            var firstPet = pets[0];
            Assert.AreEqual(firstPet.Name, "פרייה");
            Assert.AreEqual(firstPet.Birthday, DateTime.Now.AddYears(-2).AddMonths(-2).Date);
            Assert.AreEqual(firstPet.Gender, Gender.Female);
            Assert.NotNull(firstPet.AnimalType);
            Assert.NotNull(firstPet.User);

            var lastPet = pets[1];
            Assert.AreEqual(lastPet.Name, "סקאי");
            Assert.AreEqual(lastPet.Birthday, DateTime.Now.AddYears(-5).Date);
            Assert.AreEqual(lastPet.Gender, Gender.Male);
            Assert.IsNotNull(lastPet.PetTraits.FirstOrDefault());
            Assert.NotNull(firstPet.AnimalType);
            Assert.NotNull(lastPet.User);
        }

        [Test]
        public async Task SpcaTest()
        {
            // ctrl r+t
            SpcaCrawler spca = new SpcaCrawler(petHandler, petRepository, unitOfWork, traitRepository, cityRepository, animalTypeRepository, userRepository, traitOptionRepository, new SpcaParser(_azureBlobHelper));
            await spca.Load(@"https://spca.co.il/%d7%90%d7%99%d7%9e%d7%95%d7%a6%d7%99%d7%9d/",null);

            var pets = spca.Parse();

            Debugger.Break();
        }

        [Test]
        public void RehovotSpa()
        {
            // ctrl r+t
            RehovotSpaCrawler spca = new RehovotSpaCrawler(petHandler, petRepository, unitOfWork, traitRepository, cityRepository, animalTypeRepository, userRepository, traitOptionRepository, new RehovotSpaParser(_azureBlobHelper));
            spca.Load(@"http://rehovotspa.org.il/our-dogs/", null);

            var pets = spca.Parse();

            Debugger.Break();
        }

        [Test]
        public void TestRehovotBirthday()
        {
            // ctrl r+t
            RehovotSpaParser p = new RehovotSpaParser(new AzureBlobHelper(new Common.ImageHelper()));

            DateTime now = DateTime.Now; // 10.7.21

            Assert.AreEqual(p.ParseAgeInYear("בת שנתיים וחודשיים."), now.AddYears(-2).AddMonths(-2).Date);
            Assert.AreEqual(p.ParseAgeInYear("בן שנה וחודש."), now.AddYears(-1).AddMonths(-1).Date);
            Assert.AreEqual(p.ParseAgeInYear("בן שנתיים ועשרה חודשים."), now.AddYears(-2).AddMonths(-10).Date);
            Assert.AreEqual(p.ParseAgeInYear("בת שנה וחודשיים."), now.AddYears(-1).AddMonths(-2).Date);
            Assert.AreEqual(p.ParseAgeInYear("בת 2 וחצי."), now.AddYears(-2).AddMonths(-6).Date);

            Assert.AreEqual(p.ParseAgeInYear("בן שנה."), now.AddYears(-1).Date);
            Assert.AreEqual(p.ParseAgeInYear("בת שנתיים."), now.AddYears(-2).Date);
            Assert.AreEqual(p.ParseAgeInYear("בת שלוש."), now.AddYears(-3).Date);

            Assert.AreEqual(p.ParseAgeInYear("בת כ10 חודשים."), now.AddYears(0).AddMonths(-10).Date);
            Assert.AreEqual(p.ParseAgeInYear("חודש"), now.AddYears(0).AddMonths(-1).Date);
            Assert.AreEqual(p.ParseAgeInYear("בן חודש"), now.AddYears(0).AddMonths(-1).Date);
            Assert.AreEqual(p.ParseAgeInYear("בת 7 חודשים"), now.AddYears(0).AddMonths(-7).Date);
            Assert.AreEqual(p.ParseAgeInYear("בן כשישה חודשים"), now.AddYears(0).AddMonths(-6).Date);
            Assert.AreEqual(p.ParseAgeInYear("בן כ6 חודשים"), now.AddYears(0).AddMonths(-6).Date);
            Assert.AreEqual(p.ParseAgeInYear("בן כחצי שנה"), now.AddYears(0).AddMonths(-6).Date);
            Assert.AreEqual(p.ParseAgeInYear("בן חצי שנה"), now.AddYears(0).AddMonths(-6).Date);


            Assert.AreEqual(p.ParseAgeInYear("2016"), now.AddYears(-5).AddMonths(0).Date);
            Assert.AreEqual(p.ParseAgeInYear("2017"), now.AddYears(-4).AddMonths(0).Date);
            Assert.AreEqual(p.ParseAgeInYear("2018"), now.AddYears(-3).AddMonths(0).Date);
            Assert.AreEqual(p.ParseAgeInYear("2014"), now.AddYears(-7).AddMonths(0).Date);

            Debugger.Break();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            string sqlConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=GetPet;Integrated Security=True";

            services.AddDbContext<GetPetDbContext>(
                options => options.UseSqlServer(sqlConnectionString));

            services.AddAutoMapper(typeof(GetPetProfile));

            var serviceProvider = services
                .AddDbContext<GetPetDbContext>(options => options.UseSqlServer(sqlConnectionString))
                .AddScoped<IPetRepository, PetRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ITraitRepository, TraitRepository>()
                .AddScoped<ICityRepository, CityRepository>()
                .AddScoped<IAnimalTypeRepository, AnimalTypeRepository>()
                .AddScoped<IGetPetDbContextSeed, GetPetDbContextSeed>()
                .AddScoped<IPetHandler, PetHandler>()
                .AddScoped<ICrawler, RehovotSpaCrawler>()
                .AddScoped<ICrawler, SpcaCrawler>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .BuildServiceProvider();
        }
    }
}