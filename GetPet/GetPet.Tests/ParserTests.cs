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
using PetAdoption.BusinessLogic.Repositories;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GetPet.Tests
{
    public class Tests
    {
        private static IPetHandler petHandler;
        private static IPetRepository petRepository;
        private static IUnitOfWork unitOfWork;
        private static ITraitRepository traitRepository;

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
        }

        [Test]
        public void MockTest()
        {
            // ctrl r+t
            var crawler = new TestCrawler<SpcaParser>(petHandler, petRepository, unitOfWork, traitRepository);
            string file = Path.Combine(Environment.CurrentDirectory, "Files\\Spca.html");

            crawler.Load(file);

            var pets = crawler.Parse();

            Assert.AreEqual(pets.Count, 22);

            var firstPet = pets[0];
            Assert.AreEqual(firstPet.Name, "פרייה");
            Assert.AreEqual(firstPet.Birthday, DateTime.Now.AddYears(-2).AddMonths(-2).Date);
            Assert.AreEqual(firstPet.Gender, Gender.Female);
            Assert.AreEqual(firstPet.AnimalTypeId, (int)AnimalType.Dog);
            Assert.NotNull(firstPet.User);

            var lastPet = pets[1];
            Assert.AreEqual(lastPet.Name, "סקאי");
            Assert.AreEqual(lastPet.Birthday, DateTime.Now.AddYears(-5).Date);
            Assert.AreEqual(lastPet.Gender, Gender.Male);
            Assert.IsNotNull(lastPet.PetTraits.FirstOrDefault());
            Assert.AreEqual(lastPet.AnimalTypeId, (int)AnimalType.Dog);
            Assert.NotNull(lastPet.User);
        }

        [Test]
        public void SpcaTest()
        {
            // ctrl r+t
            SpcaCrawler spca = new SpcaCrawler(petHandler, petRepository, unitOfWork, traitRepository);
            spca.Load(@"https://spca.co.il/%d7%90%d7%99%d7%9e%d7%95%d7%a6%d7%99%d7%9d/");

            var pets = spca.Parse();

            Debugger.Break();
        }

        [Test]
        public void RehovotSpa()
        {
            // ctrl r+t
            RehovotSpaCrawler spca = new RehovotSpaCrawler(petHandler, petRepository, unitOfWork, traitRepository);
            spca.Load(@"http://rehovotspa.org.il/our-dogs/");

            var pets = spca.Parse();

            Debugger.Break();
        }


        [Test]
        public void TestRehovotBirthday()
        {
            // ctrl r+t
            RehovotSpaParser p = new RehovotSpaParser();

            DateTime now = DateTime.Now; // 10.7.21

            Assert.AreEqual(p.ParseAgeInYear("בת שנתיים וחודשיים."), now.AddYears(-2).AddMonths(-2).Date);
            Assert.AreEqual(p.ParseAgeInYear("בן שנה וחודש."), now.AddYears(-1).AddMonths(-1).Date);
            Assert.AreEqual(p.ParseAgeInYear("בן שנתיים ועשרה חודשים."), now.AddYears(-2).AddMonths(-10).Date);
            Assert.AreEqual(p.ParseAgeInYear("בת שנה וחודשיים."), now.AddYears(-1).AddMonths(-2).Date);
            Assert.AreEqual(p.ParseAgeInYear("בת 2 וחצי."), now.AddYears(-2).AddMonths(-6).Date);

            Assert.AreEqual(p.ParseAgeInYear("בן שנה."), now.AddYears(-1).Date);
            Assert.AreEqual(p.ParseAgeInYear("בת שנתיים."), now.AddYears(-2).Date);
            Assert.AreEqual(p.ParseAgeInYear("בת שלוש."), now.AddYears(-3).Date);
            Debugger.Break();
        }


        //[Test]
        //[Ignore("Wix is doing troubles")]
        //public void SpcaRamatGanTest()
        //{
        //    // ctrl r+t
        //    SpcaRamatGanCrawler spca = new SpcaRamatGanCrawler();
        //    spca.Load(@"https://www.spca.org.il/adopt-a-dog");

        //    var pets = spca.Parse();

        //    Debugger.Break();
        //}

        private void ConfigureServices(IServiceCollection services)
        {
            // TODO - read from tests config
            string sqlConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=GetPet;Integrated Security=True";

            services.AddDbContext<GetPetDbContext>(
                options => options.UseSqlServer(sqlConnectionString));

            services.AddAutoMapper(typeof(GetPetProfile));

            var serviceProvider = services
                .AddDbContext<GetPetDbContext>(options => options.UseSqlServer(sqlConnectionString))
                .AddScoped<IPetRepository, PetRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ITraitRepository, TraitRepository>()
                .AddScoped<IGetPetDbContextSeed, GetPetDbContextSeed>()
                .AddScoped<IPetHandler, PetHandler>()
                .AddScoped<ICrawler, RehovotSpaCrawler>()
                .AddScoped<ICrawler, SpcaCrawler>()
                //.AddScoped<ICrawler, SpcaRamatGanCrawler>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .BuildServiceProvider();
        }
    }
}