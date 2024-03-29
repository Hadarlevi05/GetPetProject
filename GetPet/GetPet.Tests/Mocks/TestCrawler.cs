﻿using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Repositories;
using GetPet.Crawler.Crawlers;
using GetPet.Crawler.Parsers.Abstractions;
using GetPet.Data.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GetPet.Tests.Mocks
{
    public class TestCrawler<T> : CrawlerBase<T> where T : IParser
    {
        public TestCrawler(
            IPetHandler petHandler,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork,
            ITraitRepository traitRepository,
            ICityRepository cityRepository,
            IAnimalTypeRepository animalTypeRepository,
            IUserRepository userRepository,
            ITraitOptionRepository traitOptionRepository,
            T parser) :
            base(petHandler, petRepository, unitOfWork, traitRepository, cityRepository, animalTypeRepository, userRepository, traitOptionRepository, parser)
        {
        }

        protected override string url => throw new NotImplementedException();
        protected override string url2 => throw new NotImplementedException();

        public override async Task Load(string url,string url2)
        {
            await Task.Delay(0);
            
            if (!File.Exists(url))
            {
                throw new Exception("Cannot find file");
            }

            using (var file = File.OpenRead(url))
            {
                _doc.Load(file);
                _parser.Document = _doc;
            }
        }

        public override async Task<User> CreateUser()
        {
            return await _userRepository.AddAsync(new User()
            {
                Name = "Testing",
                Email = "Testing@gmail.com",
                UserType = Data.Enums.UserType.Organization,
                Organization = new Organization()
                {
                    Name = "Testing",
                    Email = "Testing@gmail.com",
                },
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                PasswordHash = "1234",
                CityId = 1,
            });
        }
    }
}
