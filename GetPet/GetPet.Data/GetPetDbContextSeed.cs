﻿using GetPet.Common;
using GetPet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.Data
{
    public interface IGetPetDbContextSeed
    {
        Task Seed();
    }
    public class GetPetDbContextSeed : IGetPetDbContextSeed
    {
        public GetPetDbContextSeed()
        {

        }

        public async Task Seed()
        {
            using var context = new GetPetDbContext();

            context.Database.EnsureCreated();

            bool dataExist = context.Cities.Any();
            if (dataExist)
                return;

            using var transaction = context.Database.BeginTransaction();

            var city1 = context.Cities.Add(new City { Name = "תל אביב", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            var city2 = context.Cities.Add(new City { Name = "כפר-סבא", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });

            await context.SaveChangesAsync();

            var dog = context.AnimalTypes.Add(new AnimalType { Name = "כלב" , CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            var cat = context.AnimalTypes.Add(new AnimalType { Name = "חתול", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            context.AnimalTypes.Add(new AnimalType { Name = "שפן", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });

            await context.SaveChangesAsync();

            var size = context.Traits.Add(new Trait { Name = "גודל" , CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            var color = context.Traits.Add(new Trait { Name = "צבע", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            context.Traits.Add(new Trait { Name = "מסתדר עם ילדים", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            context.Traits.Add(new Trait { Name = "מאולף", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });

            await context.SaveChangesAsync();

            context.AnimalTraits.Add(new AnimalTrait { Trait = size.Entity, AnimalType = dog.Entity, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            context.AnimalTraits.Add(new AnimalTrait { Trait = size.Entity, AnimalType = cat.Entity, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });

            await context.SaveChangesAsync();


            PasswordHashHelper hash = new PasswordHashHelper("password");
            var hashPassword = System.Text.Encoding.Default.GetString(hash.Hash);

            var hadar = context.Users.Add(new User
            {
                CityId = city1.Entity.Id,
                Name = "הדר",
                Email = "hadar@getpet.co.il",
                UserType = Enums.UserType.Regular,
                EmailSubscription = true,
                PasswordHash = hashPassword,
                CreationTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            context.Pets.Add(new Pet
            {
                Name = "לואי",
                AnimalTypeId = dog.Entity.Id,
                Birthday = DateTime.UtcNow.AddYears(-2),
                Gender = Enums.Gender.Male,
                Status = Enums.PetStatus.WaitingForAdoption,
                UserId = hadar.Entity.Id,
                Description = "לואי שלנו הוא כלב חברותי ואהוב על כל הבריות, אוהב לשחק מאוד ונהדר עם ילדים נשים ותף",
                Traits = new List<PetTrait>()
                {
                    new PetTrait{ TraitId = size.Entity.Id, Value = "גדול", CreationTimestamp= DateTime.UtcNow, UpdatedTimestamp=DateTime.UtcNow},
                    new PetTrait{ TraitId = color.Entity.Id, Value = "בז'", CreationTimestamp= DateTime.UtcNow, UpdatedTimestamp=DateTime.UtcNow }
                },
                MetaFileLinks = new List<MetaFileLink>()
                {
                    new MetaFileLink{ MimeType= "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_1.jpg", CreationTimestamp= DateTime.UtcNow, UpdatedTimestamp=DateTime.UtcNow},
                    new MetaFileLink{ MimeType= "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_2.jpg", CreationTimestamp= DateTime.UtcNow, UpdatedTimestamp=DateTime.UtcNow},
                    new MetaFileLink{ MimeType= "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_3.jpg", CreationTimestamp= DateTime.UtcNow, UpdatedTimestamp=DateTime.UtcNow},

                },
                CreationTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
    }
}