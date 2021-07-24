﻿using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.MappingProfiles;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Crawlers;
using GetPet.Crawler.Crawlers.Abstractions;
using GetPet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetAdoption.BusinessLogic.Repositories;
using System;
using System.IO;

namespace GetPet.CrawlerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = Initialize();

            LaunchCrawlers(serviceProvider);
        }

        private static ServiceProvider Initialize()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddAutoMapper(typeof(GetPetProfile));

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            string sqlConnectionString = configuration.GetConnectionString("GetPetConnectionString");

            var serviceProvider = services
                .AddDbContext<GetPetDbContext>(options =>
                    {
                        options.UseSqlServer(sqlConnectionString);
                        options.EnableSensitiveDataLogging();
                    }
                )
                .AddScoped<IPetRepository, PetRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IGetPetDbContextSeed, GetPetDbContextSeed>()
                .AddScoped<ITraitRepository, TraitRepository>()
                .AddScoped<ICityRepository, CityRepository>()
                .AddScoped<IAnimalTypeRepository, AnimalTypeRepository>()
                .AddScoped<IPetHandler, PetHandler>()
                .AddScoped<ICrawler, RehovotSpaCrawler>()
                .AddScoped<ICrawler, SpcaCrawler>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .BuildServiceProvider();
            return serviceProvider;
        }

        private static void LaunchCrawlers(ServiceProvider serviceProvider)
        {
            var crawlers = serviceProvider.GetServices<ICrawler>();

            foreach (var crawler in crawlers)
            {
                Console.WriteLine($"Working on {crawler.GetType()}");

                crawler.Load();
                var result = crawler.Parse();
                crawler.InsertToDB(result);
            }
        }
    }
}