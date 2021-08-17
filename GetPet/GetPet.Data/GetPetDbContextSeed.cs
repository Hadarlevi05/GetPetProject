using GetPet.Common;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

            var cities = GetStringResource("GetPet.Data.StaticData.Cities.txt")
                .Split(Environment.NewLine.ToCharArray())
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Select(c => c.Trim());

            foreach (var city in cities)
            {
                context.Cities.Add(new City { Name = city, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now });
            }

            await context.SaveChangesAsync();

            var telAviv = context.Cities.SingleOrDefault(c => c.Name.Contains("תל אביב"));

            await context.SaveChangesAsync();

            var animalTypeList = new[] {
                "כלבים",
                "חתולים"
            };

            foreach (var item in animalTypeList)
            {
                context.AnimalTypes.Add(new AnimalType { Name = item, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now });
                await context.SaveChangesAsync();
            }

            var dog = context.AnimalTypes.FirstOrDefault();
            var cat = context.AnimalTypes.Skip(1).Take(1).FirstOrDefault();

            await context.SaveChangesAsync();

            // Dogs
            var size = context.Traits.Add(new Trait { Name = "גודל", FemaleName = "גודל", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Values });
            var color = context.Traits.Add(new Trait { Name = "צבע", FemaleName = "צבע", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Values });
            var age = context.Traits.Add(new Trait { Name = "גיל", FemaleName = "גיל", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Values });
            var gender = context.Traits.Add(new Trait { Name = "מין", FemaleName = "מין", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Values });

            var goodWithKids = context.Traits.Add(new Trait { Name = "מסתדר עם ילדים", FemaleName = "מסתדרת עם ילדים", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var goodWithPpl = context.Traits.Add(new Trait { Name = "מסתדר עם אנשים", FemaleName = "מסתדרת עם אנשים", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var goodWithDogs = context.Traits.Add(new Trait { Name = "מסתדר עם כלבים", FemaleName = "מסתדרת עם כלבים", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var trained = context.Traits.Add(new Trait { Name = "מאולף", FemaleName = "מאולפת", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var worried = context.Traits.Add(new Trait { Name = "חששן", FemaleName = "חששנית", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var vaccinated = context.Traits.Add(new Trait { Name = "מחוסן", FemaleName = "מחוסנת", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var mixed = context.Traits.Add(new Trait { Name = "מעורב", FemaleName = "מעורבת", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var sterilized = context.Traits.Add(new Trait { Name = "מעוקר", FemaleName = "מעוקרת", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var SuitableForGarden = context.Traits.Add(new Trait { Name = "מתאים לחצר", FemaleName = "מתאימה לחצר", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var lovesCaresses = context.Traits.Add(new Trait { Name = "אוהב ליטופים", FemaleName = "אוהבת ליטופים", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var arangti = context.Traits.Add(new Trait { Name = "אנרגטי", FemaleName = "אנרגטית", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var sociable = context.Traits.Add(new Trait { Name = "חברותי", FemaleName = "חברותית", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var quiet = context.Traits.Add(new Trait { Name = "שקט", FemaleName = "שקטה", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var gentle = context.Traits.Add(new Trait { Name = "עדין", FemaleName = "עדינה", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var relax = context.Traits.Add(new Trait { Name = "רגוע", FemaleName = "רגועה", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var tolerant = context.Traits.Add(new Trait { Name = "סובלני", FemaleName = "סובלנית", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var homey = context.Traits.Add(new Trait { Name = "ביתי", FemaleName = "ביתית", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var accustomedToNeeds = context.Traits.Add(new Trait { Name = "מתורגל לצרכים", FemaleName = "מתורגלת לצרכים", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var racial = context.Traits.Add(new Trait { Name = "גזעי", FemaleName = "גזעית", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var likeToPlay = context.Traits.Add(new Trait { Name = "אוהב לשחק", FemaleName = "אוהבת לשחק", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var smart = context.Traits.Add(new Trait { Name = "חכם", FemaleName = "חכמה", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var loyal = context.Traits.Add(new Trait { Name = "נאמן", FemaleName = "נאמנה", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var happy = context.Traits.Add(new Trait { Name = "שמח", FemaleName = "שמחה", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var funny = context.Traits.Add(new Trait { Name = "מצחיק", FemaleName = "מצחיקה", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var gamin = context.Traits.Add(new Trait { Name = "שובב", FemaleName = "שובבה", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var disciplined = context.Traits.Add(new Trait { Name = "ממושמע", FemaleName = "ממושמעת", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });

            // Cats
            var colorCat = context.Traits.Add(new Trait { Name = "צבע", FemaleName = "צבע", AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Values });
            var ageCat = context.Traits.Add(new Trait { Name = "גיל", FemaleName = "גיל", AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Values });
            var genderCat = context.Traits.Add(new Trait { Name = "מין", FemaleName = "מין", AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Values });

            var catCurious = context.Traits.Add(new Trait { Name = "סקרן", FemaleName = "סקרנית", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catBrave = context.Traits.Add(new Trait { Name = "אמיץ", FemaleName = "אמיצה", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catSociable = context.Traits.Add(new Trait { Name = "חברותי", FemaleName = "חברותית", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catArangti = context.Traits.Add(new Trait { Name = "אנרגטי", FemaleName = "אנרגטית", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catLovesCaresses = context.Traits.Add(new Trait { Name = "אוהב ליטופים", FemaleName = "אוהבת ליטופים", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catQuiet = context.Traits.Add(new Trait { Name = "שקט", FemaleName = "שקטה", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catGentle = context.Traits.Add(new Trait { Name = "עדין", FemaleName = "עדינה", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catRelax = context.Traits.Add(new Trait { Name = "רגוע", FemaleName = "רגועה", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catTolerant = context.Traits.Add(new Trait { Name = "סובלני", FemaleName = "סובלני", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catHomey = context.Traits.Add(new Trait { Name = "ביתי", FemaleName = "ביתית", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catAccustomedToSandbox = context.Traits.Add(new Trait { Name = "מורגל לארגז חול", FemaleName = "מורגלת לארגז חול", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catRacial = context.Traits.Add(new Trait { Name = "גזעי", FemaleName = "גזעית", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catLikeToPlay = context.Traits.Add(new Trait { Name = "אוהב לשחק", FemaleName = "אוהבת לשחק", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catSmart = context.Traits.Add(new Trait { Name = "חכם", FemaleName = "חכמה", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catLoyal = context.Traits.Add(new Trait { Name = "נאמן", FemaleName = "נאמנה", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catHappy = context.Traits.Add(new Trait { Name = "שמח", FemaleName = "שמחה", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catFunny = context.Traits.Add(new Trait { Name = "מצחיק", FemaleName = "מצחיקה", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catGamin = context.Traits.Add(new Trait { Name = "שובב", FemaleName = "שובבה", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });

            await context.SaveChangesAsync();

            var traitOptionSmall = context.TraitOptions.Add(new TraitOption { Option = "קטן", FemaleOption = "קטנה", TraitId = size.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "בינוני", FemaleOption = "בינונית", TraitId = size.Entity.Id });
            var traitOptionBig = context.TraitOptions.Add(new TraitOption { Option = "גדול", FemaleOption = "גדול", TraitId = size.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "ענק", FemaleOption = "ענקית", TraitId = size.Entity.Id });

            // גור עד 9 חודשים
            // צעיר - 9-24 חודשים
            // בוגר 2-7 שנים
            // מבוגר - מעל 7 שנים
            context.TraitOptions.Add(new TraitOption { Option = "גור", FemaleOption = "גורה", TraitId = age.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "צעיר", FemaleOption = "צעירה", TraitId = age.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "בוגר", FemaleOption = "בוגרת", TraitId = age.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "מבוגר", FemaleOption = "מבוגרת", TraitId = age.Entity.Id });

            context.TraitOptions.Add(new TraitOption { Option = "גור", FemaleOption = "גורה", TraitId = ageCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "צעיר", FemaleOption = "צעירה", TraitId = ageCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "בוגר", FemaleOption = "בוגרת", TraitId = ageCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "מבוגר", FemaleOption = "מבוגרת", TraitId = ageCat.Entity.Id });

            context.TraitOptions.Add(new TraitOption { Option = "זכר", FemaleOption = "זכר", TraitId = gender.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "נקבה", FemaleOption = "נקבה", TraitId = gender.Entity.Id });

            context.TraitOptions.Add(new TraitOption { Option = "לבן", FemaleOption = "לבנה", TraitId = color.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "שחור", FemaleOption = "שחורה", TraitId = color.Entity.Id });
            var colorBeige = context.TraitOptions.Add(new TraitOption { Option = "בז'", FemaleOption = "בז'", TraitId = color.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "דלמטי", FemaleOption = "דלמטית", TraitId = color.Entity.Id });


            context.TraitOptions.Add(new TraitOption { Option = "לבן", FemaleOption = "לבנה", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "שחור", FemaleOption = "שחורה", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "בז'", FemaleOption = "בז'", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "ג'ינג'י", FemaleOption = "ג'ינג'ית", TraitId = colorCat.Entity.Id });

            context.TraitOptions.Add(new TraitOption { Option = "זכר", FemaleOption = "זכר", TraitId = genderCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "נקבה", FemaleOption = "נקבה", TraitId = genderCat.Entity.Id });

            await context.SaveChangesAsync();

            AddBooleanTraitOption(context, goodWithKids);
            AddBooleanTraitOption(context, goodWithPpl);
            AddBooleanTraitOption(context, goodWithDogs);
            AddBooleanTraitOption(context, trained);
            AddBooleanTraitOption(context, worried);
            AddBooleanTraitOption(context, vaccinated);
            AddBooleanTraitOption(context, mixed);
            AddBooleanTraitOption(context, sterilized);
            AddBooleanTraitOption(context, SuitableForGarden);
            AddBooleanTraitOption(context, lovesCaresses);
            AddBooleanTraitOption(context, arangti);
            AddBooleanTraitOption(context, sociable);
            AddBooleanTraitOption(context, quiet);
            AddBooleanTraitOption(context, gentle);
            AddBooleanTraitOption(context, relax);
            AddBooleanTraitOption(context, tolerant);
            AddBooleanTraitOption(context, homey);
            AddBooleanTraitOption(context, accustomedToNeeds);
            AddBooleanTraitOption(context, racial);
            AddBooleanTraitOption(context, likeToPlay);
            AddBooleanTraitOption(context, smart);
            AddBooleanTraitOption(context, loyal);
            AddBooleanTraitOption(context, happy);
            AddBooleanTraitOption(context, funny);
            AddBooleanTraitOption(context, gamin);
            AddBooleanTraitOption(context, disciplined);
            //cat
            AddBooleanTraitOption(context, catCurious);
            AddBooleanTraitOption(context, catBrave);
            AddBooleanTraitOption(context, catSociable);
            AddBooleanTraitOption(context, catArangti);
            AddBooleanTraitOption(context, catLovesCaresses);
            AddBooleanTraitOption(context, catQuiet);
            AddBooleanTraitOption(context, catGentle);
            AddBooleanTraitOption(context, catRelax);
            AddBooleanTraitOption(context, catTolerant);
            AddBooleanTraitOption(context, catHomey);
            AddBooleanTraitOption(context, catAccustomedToSandbox);
            AddBooleanTraitOption(context, catRacial);
            AddBooleanTraitOption(context, catLikeToPlay);
            AddBooleanTraitOption(context, catSmart);
            AddBooleanTraitOption(context, catLoyal);
            AddBooleanTraitOption(context, catHappy);
            AddBooleanTraitOption(context, catFunny);
            AddBooleanTraitOption(context, catGamin);

            await context.SaveChangesAsync();

            var system = context.Users.Add(new User
            {
                CityId = telAviv.Id,
                Name = "מערכת",
                Email = "support@getpet.co.il",
                UserType = Enums.UserType.Admin,
                EmailSubscription = true,
                PasswordHash = SecurePasswordHasher.Hash("123456"),
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now
            });

            var hadar = context.Users.Add(new User
            {
                CityId = telAviv.Id,
                Name = "הדר",
                Email = "hadar@getpet.co.il",
                UserType = Enums.UserType.Admin,
                EmailSubscription = true,
                PasswordHash = SecurePasswordHasher.Hash("123456"),
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now
            });

            var sharon = context.Users.Add(new User
            {
                CityId = telAviv.Id,
                Name = "שרון",
                Email = "sharon@getpet.co.il",
                UserType = Enums.UserType.Admin,
                EmailSubscription = true,
                PasswordHash = SecurePasswordHasher.Hash("123456"),
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now
            });

            var liza = context.Users.Add(new User
            {
                CityId = telAviv.Id,
                Name = "ליזה",
                Email = "liza@getpet.co.il",
                UserType = Enums.UserType.Admin,
                EmailSubscription = true,
                PasswordHash = SecurePasswordHasher.Hash("123456"),
                CreationTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now
            });


            await context.SaveChangesAsync();

            var images = new[] {
                new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_1.jpg", CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now },
                new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_2.jpg", CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now },
                new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_3.jpg", CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now },
            };
            var names = new[] {
                "לואי",
                //"לואון",
                //"לואיש"
            };
            var descriptions = new[] {
                "לואי שלנו הוא כלב חברותי ואהוב על כל הבריות, אוהב לשחק מאוד ונהדר עם ילדים נשים וטף",
                //"כלב חתיך ויפיוף",
                //"לואי לואי לואי לורם יפסום"
            };

            for (int i = 0; i < names.Length; i++)
            {
                context.Pets.Add(new Pet
                {
                    Name = names[i],
                    AnimalTypeId = dog.Id,
                    Birthday = DateTime.Now.AddYears(-2),
                    Gender = Enums.Gender.Male,
                    Status = Enums.PetStatus.Adopted,
                    UserId = system.Entity.Id,
                    Description = descriptions[i],
                    MetaFileLinks = images.ToList(),
                    CreationTimestamp = DateTime.Now,
                    UpdatedTimestamp = DateTime.Now,
                    Source = Enums.PetSource.Internal,
                });
            }

            await context.SaveChangesAsync();

            foreach (var pet in context.Pets.ToList())
            {
                context.PetHistoryStatuses.Add(new PetHistoryStatus
                {
                    PetId = pet.Id,
                    Status = Enums.PetStatus.Created
                });
                await context.SaveChangesAsync();
                context.PetHistoryStatuses.Add(new PetHistoryStatus
                {
                    PetId = pet.Id,
                    Status = Enums.PetStatus.WaitingForAdoption
                });
                await context.SaveChangesAsync();
                context.PetHistoryStatuses.Add(new PetHistoryStatus
                {
                    PetId = pet.Id,
                    Status = Enums.PetStatus.Adopted
                });
            }

            await context.SaveChangesAsync();

            var booleanTraits = await context.TraitOptions.Where(to => to.TraitId == goodWithDogs.Entity.Id || to.TraitId == vaccinated.Entity.Id).ToListAsync();

            foreach (var pet in context.Pets)
            {
                pet.PetTraits = new List<PetTrait>
                {
                    new PetTrait { PetId = pet.Id, Trait = size.Entity, TraitOptionId = traitOptionBig.Entity.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now },
                    new PetTrait { PetId = pet.Id, Trait = color.Entity, TraitOptionId = colorBeige.Entity.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now },
                    new PetTrait { PetId = pet.Id, Trait = goodWithDogs.Entity,CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now ,
                        TraitOptionId = booleanTraits.First(bt => bt.TraitId == goodWithDogs.Entity.Id && bt.Option == "כן").Id },
                    new PetTrait { PetId = pet.Id, Trait = vaccinated.Entity, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now,
                        TraitOptionId = booleanTraits.First(bt => bt.TraitId == vaccinated.Entity.Id && bt.Option == "כן").Id }
                };
            }

            await context.SaveChangesAsync();

            var articleTitles = new[] {
                "גם לי מגיע לחיות",
                "מהי תולעת הפארק? למה לשים לב ואיך לטפל?",
                "לאמץ כלב חדש זו הנאה אדירה. בואו ללמוד את השלבים הראשונים והדגשים שחשוב לדעת",
                "30 בחודש – תורמים לבעלי החיים",
                "קר שם בחוץ"
            };

            for (int i = 1; i <= 5; i++)
            {
                var mtf = context.MetaFileLinks.Add(new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/article{i}.jpg", CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now });

                await context.SaveChangesAsync();

                context.Articles.Add(new Article
                {
                    Title = articleTitles[i - 1],
                    Content = @"כמה שווים חיים של חתולה?
                    בעוד ברחבי העולם חתולים וכלבים מקבלים מעמד המכיר בהם כיצורים רגישים ובעלי זכויות, אצלנו חייו של חתול הפכו מזמן לשיקולים צרים של רווח והפסד.
                    לפני מספר חודשים, קיבלנו פניה מפעילים בעפולה, לפיה המחלקה הווטרינרית פינתה גורת חתולים עם בעיות חמורות בעיניה. הפעילים הציפו באותו היום ובמהלך סוף השבוע לאחריו את המוקד העירוני בטלפונים ובהודעות, בניסיון לדעת מה עלה בגורל החתולה. כמה פעילים הציעו לשאת בעלויות הטיפול או לקחת אותה תחת חסותם ולדאוג לטיפול בה. רק ביום א’ התקבלה התשובה שהחתולה הומתה “המתת חסד”. לא נמסר לאיזו מרפאה הועברה החתולה, ומה היו השיקולים להמתתה. הפעילים הבינו מיד כי משהו אינו כשורה, וכי העירייה לא מספרת את כל הסיפור. בעקבות זאת הוצאנו מכתב בהול ובקשת חופש מידע לעירייה בנוגע לחתולה.
                    רק לאחר שלושה חודשים התברר כי ניתן היה לאשפז את החתולה, ובהתאם להתקדמות הטיפול, למסור אותה לאימוץ לבית חם. אולם בעירייה החליטו להמית אותה בשל “העלות הגבוהה של הטיפול ועקב כך שלחתול אין בעלים”! זאת, למרות הפעילים הרבים שהציעו לממן את הטיפול.
                    אז כמה שווים חייה של חתולה? לא פעם רשויות מקומיות קובעות מחיר כמעט אפסי – לפעמים אפילו הימנעות מחוסר נוחות תספיק. הגיע הזמן שכל הרשויות המקומיות בישראל יבינו כי לכל בעלי החיים, גם הפצועים וה”קשים”, מגיע לחיות!
                    מקור:           
                    https://www.letlive.org.il/?p=43555
                    ",
                    UserId = hadar.Entity.Id,
                    MetaFileLinkId = mtf.Entity.Id,
                    Comments = new List<Comment> {
                        new Comment {
                            Text ="לכל בעל חיים מגיע לחיות!",
                            UserId = liza.Entity.Id,
                        },
                        new Comment {
                            Text ="כל הכבוד לכל הפעילים.",
                            UserId = sharon.Entity.Id,
                        }
                    }
                });
            }
            await context.SaveChangesAsync();

            await transaction.CommitAsync();
        }

        private static void AddBooleanTraitOption(GetPetDbContext context, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Trait> traitEntry)
        {
            context.TraitOptions.Add(new TraitOption { Option = "כן", FemaleOption = "כן", TraitId = traitEntry.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "לא", FemaleOption = "לא", TraitId = traitEntry.Entity.Id });
        }

        public string GetStringResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}