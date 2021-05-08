using GetPet.Common;
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

            var dog = context.AnimalTypes.Add(new AnimalType { Name = "כלב", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            var cat = context.AnimalTypes.Add(new AnimalType { Name = "חתול", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            context.AnimalTypes.Add(new AnimalType { Name = "שפן", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });

            await context.SaveChangesAsync();

            var size = context.Traits.Add(new Trait { Name = "גודל", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            var color = context.Traits.Add(new Trait { Name = "צבע", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            var goodWithKids = context.Traits.Add(new Trait { Name = "מסתדר עם ילדים", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            var trained = context.Traits.Add(new Trait { Name = "מאולף", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });

            await context.SaveChangesAsync();

            context.TraitOptions.Add(new TraitOption { Option = "קטן", TraitId = size.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "בינוני", TraitId = size.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "גדול", TraitId = size.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "ענק", TraitId = size.Entity.Id });

            context.TraitOptions.Add(new TraitOption { Option = "לבן", TraitId = color.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "שחור", TraitId = color.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "בז'", TraitId = color.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "דלמטי", TraitId = color.Entity.Id });

            context.TraitOptions.Add(new TraitOption { Option = "כן", TraitId = goodWithKids.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "לא", TraitId = goodWithKids.Entity.Id });

            context.TraitOptions.Add(new TraitOption { Option = "כן", TraitId = trained.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "לא", TraitId = trained.Entity.Id });

            await context.SaveChangesAsync();

            context.AnimalTraits.Add(new AnimalTrait { Trait = size.Entity, AnimalType = dog.Entity, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            context.AnimalTraits.Add(new AnimalTrait { Trait = size.Entity, AnimalType = cat.Entity, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });

            await context.SaveChangesAsync();

            var hadar = context.Users.Add(new User
            {
                CityId = city1.Entity.Id,
                Name = "מערכת",
                Email = "hadar@getpet.co.il",
                UserType = Enums.UserType.Admin,
                EmailSubscription = true,
                PasswordHash = SecurePasswordHasher.Hash("password"),
                CreationTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            var images = new[] {
                new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_1.jpg", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow },
                new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_2.jpg", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow },
                new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_3.jpg", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow },
            };
            var names = new[] {
                "לואי",
                "לואון",
                "לואיש"
            };
            var descriptions = new[] {
                "לואי שלנו הוא כלב חברותי ואהוב על כל הבריות, אוהב לשחק מאוד ונהדר עם ילדים נשים ותף",
                "כלב חתיך ויפיוף",
                "לואי לואי לואי לורם יפסום"
            };

            for (int i = 0; i < names.Length; i++)
            {
                context.Pets.Add(new Pet
                {
                    Name = names[i],
                    AnimalTypeId = dog.Entity.Id,
                    Birthday = DateTime.UtcNow.AddYears(-2),
                    Gender = Enums.Gender.Male,
                    Status = Enums.PetStatus.WaitingForAdoption,
                    UserId = hadar.Entity.Id,
                    Description = descriptions[i],
                    //PetTraits = new List<PetTrait>()
                    //{
                    //    new PetTrait{ Trait = size.Entity,  Description = "גדול", CreationTimestamp= DateTime.UtcNow, UpdatedTimestamp=DateTime.UtcNow},
                    //    new PetTrait{ Trait = color.Entity, Description = "בז'", CreationTimestamp= DateTime.UtcNow, UpdatedTimestamp=DateTime.UtcNow }
                    //},
                    MetaFileLinks = new List<MetaFileLink>()
                    {
                        images[i]

                    },
                    CreationTimestamp = DateTime.UtcNow,
                    UpdatedTimestamp = DateTime.UtcNow
                });
            }

            await context.SaveChangesAsync();

            foreach (var pet in context.Pets)
            {
                pet.PetTraits = new List<PetTrait>();

                pet.PetTraits.Add(new PetTrait { PetId = pet.Id, Trait = size.Entity, Description = "גדול", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
                pet.PetTraits.Add(new PetTrait { PetId = pet.Id, Trait = color.Entity, Description = "בז'", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });

            }


            await transaction.CommitAsync();
        }
    }
}