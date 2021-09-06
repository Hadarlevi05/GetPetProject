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
            var sterilized = context.Traits.Add(new Trait { Name = "מסורס", FemaleName = "מעוקרת", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
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
            var mischievous = context.Traits.Add(new Trait { Name = "שובב", FemaleName = "שובבה", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var disciplined = context.Traits.Add(new Trait { Name = "ממושמע", FemaleName = "ממושמעת", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var blind = context.Traits.Add(new Trait { Name = "עיוור", FemaleName = "עיוורת", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var curious = context.Traits.Add(new Trait { Name = "סקרן", FemaleName = "סקרנית", IsBoolean = true, AnimalTypeId = dog.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });


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
            var catMischievous = context.Traits.Add(new Trait { Name = "שובב", FemaleName = "שובבה", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catSterilized = context.Traits.Add(new Trait { Name = "מסורס", FemaleName = "מעוקרת", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catVaccinated = context.Traits.Add(new Trait { Name = "מחוסן", FemaleName = "מחוסנת", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catGoodWithKids = context.Traits.Add(new Trait { Name = "מסתדר עם ילדים", FemaleName = "מסתדרת עם ילדים", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catGoodWithPpl = context.Traits.Add(new Trait { Name = "מסתדר עם אנשים", FemaleName = "מסתדרת עם אנשים", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catGoodWithDogs = context.Traits.Add(new Trait { Name = "מסתדר עם כלבים", FemaleName = "מסתדרת עם כלבים", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catCaress = context.Traits.Add(new Trait { Name = "מתלטף", FemaleName = "מתלטפת", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catWormTreated = context.Traits.Add(new Trait { Name = "מתולע", FemaleName = "מתולעת", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catFleaTreated = context.Traits.Add(new Trait { Name = "מפורעש", FemaleName = "מפורעשת", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catMixed = context.Traits.Add(new Trait { Name = "מעורב", FemaleName = "מעורבת", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });
            var catBlind = context.Traits.Add(new Trait { Name = "עיוור", FemaleName = "עיוורת", IsBoolean = true, AnimalTypeId = cat.Id, CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now, TraitType = Enums.TraitType.Boolean });


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
            context.TraitOptions.Add(new TraitOption { Option = "חום", FemaleOption = "חומה", TraitId = color.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "אפור", FemaleOption = "אפורה", TraitId = color.Entity.Id });


            context.TraitOptions.Add(new TraitOption { Option = "לבן", FemaleOption = "לבנה", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "שחור", FemaleOption = "שחורה", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "ג'ינג'י", FemaleOption = "ג'ינג'ית", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "טריקולור", FemaleOption = "טריקולורית", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "אפור", FemaleOption = "אפורה", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "טאבי", FemaleOption = "טאבי", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "טורטי", FemaleOption = "טורטי", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "טאבי", FemaleOption = "טאבי", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "שחור לבן", FemaleOption = "שחורה לבנה", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "ג'ינג'י לבן", FemaleOption = "ג'ינג'ית לבנה", TraitId = colorCat.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "אפור לבן", FemaleOption = "אפורה לבנה", TraitId = colorCat.Entity.Id });

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
            AddBooleanTraitOption(context, mischievous);
            AddBooleanTraitOption(context, disciplined);
            AddBooleanTraitOption(context, blind);
            AddBooleanTraitOption(context, curious);
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
            AddBooleanTraitOption(context, catMischievous);
            AddBooleanTraitOption(context, catSterilized);
            AddBooleanTraitOption(context, catVaccinated);
            AddBooleanTraitOption(context, catGoodWithKids);
            AddBooleanTraitOption(context, catGoodWithPpl);
            AddBooleanTraitOption(context, catGoodWithDogs);
            AddBooleanTraitOption(context, catCaress);
            AddBooleanTraitOption(context, catWormTreated);
            AddBooleanTraitOption(context, catFleaTreated);
            AddBooleanTraitOption(context, catMixed);
            AddBooleanTraitOption(context, catBlind);


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
                new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"upload-content/8c996882-d950-4828-bee6-cc8b20f0132a.jpg", CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now },
                //new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_2.jpg", CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now },
                //new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/img_3.jpg", CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now },
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
                "לאמץ כלב חדש זו הנאה אדירה. בואו ללמוד את השלבים הראשונים והדגשים שחשוב לדעת",
                "מדוע לאמץ חיות מחמד",
                "מדוע לאמץ כלב ולא לקנות?",
            };

            var articleImages = new[] {
                "upload-content/f7c8d80e-ee27-4492-b243-4610ca21551f.jpg",
                "upload-content/f3c68f4e-3388-4a19-8cfd-0ba603afbd04.jpg",
                "upload-content/78411743-4ec5-4e24-b2c1-200de3701276.jpg",
                "upload-content/04edea11-5d74-4189-a38d-ad78cc878d90.jpg",
            };

            var articleContent = new[] {
                @"<p>
                    כמה שווים חיים של חתולה?
                    </p><p>
                    בעוד ברחבי העולם חתולים וכלבים מקבלים מעמד המכיר בהם כיצורים רגישים ובעלי זכויות, אצלנו חייו של חתול הפכו מזמן לשיקולים צרים של רווח והפסד.
                    </p><p>
                    לפני מספר חודשים, קיבלנו פניה מפעילים בעפולה, לפיה המחלקה הווטרינרית פינתה גורת חתולים עם בעיות חמורות בעיניה. הפעילים הציפו באותו היום ובמהלך סוף השבוע לאחריו את המוקד העירוני בטלפונים ובהודעות, בניסיון לדעת מה עלה בגורל החתולה. כמה פעילים הציעו לשאת בעלויות הטיפול או לקחת אותה תחת חסותם ולדאוג לטיפול בה. רק ביום א’ התקבלה התשובה שהחתולה הומתה “המתת חסד”. לא נמסר לאיזו מרפאה הועברה החתולה, ומה היו השיקולים להמתתה. הפעילים הבינו מיד כי משהו אינו כשורה, וכי העירייה לא מספרת את כל הסיפור. בעקבות זאת הוצאנו מכתב בהול ובקשת חופש מידע לעירייה בנוגע לחתולה.
                    </p><p>
                    רק לאחר שלושה חודשים התברר כי ניתן היה לאשפז את החתולה, ובהתאם להתקדמות הטיפול, למסור אותה לאימוץ לבית חם. אולם בעירייה החליטו להמית אותה בשל “העלות הגבוהה של הטיפול ועקב כך שלחתול אין בעלים”! זאת, למרות הפעילים הרבים שהציעו לממן את הטיפול.
                    </p><p>
                    אז כמה שווים חייה של חתולה? לא פעם רשויות מקומיות קובעות מחיר כמעט אפסי – לפעמים אפילו הימנעות מחוסר נוחות תספיק. הגיע הזמן שכל הרשויות המקומיות בישראל יבינו כי לכל בעלי החיים, גם הפצועים וה”קשים”, מגיע לחיות!
                    </p><p>
                    נלקח מ:
                    
                    https://www.letlive.org.il/?p=43555
                    </p>",
                @"
                    <p>
                    חינוך כלבים לצרכים קודם כל צריך להבין שגור קטן, בדיוק כמו תינוק לא יכול להתאפק יותר מדי. כך למשל, גור בן 3 חודשים יכול להתאפק עד 4 שעות. התאפקות זו לא פעולה טבעית עבור כלב 
                    </p><p>
                    ועלינו כבעלים שלו לחנך אותו להתאפק. לכן, צריך להוציא את הגור מספר רב של פעמים במהלך היום ופעם נוספת בלילה. כך נדע מתי הגור מתכנן לעשות את צרכיו: גורים לרוב עושים צרכים לאחר שינה, אוכל, שתייה, משחק והשתוללות. אפשר גם לזהות גור שמתכנן לעשות פיפי או קקי באמצעות סימנים מקדימים, כמו: רחרוח הרצפה, סיבובים ואי שקט. זה הזמן שלכם לקחת את הגור ולהוציא אותו החוצה למקום קבוע בסמוך לבית (בתנאי שסיים את חיסוניו כמובן). כדי שנוכל לעקוב ולשלוט על זמני עשיית הצרכים של הגור, חשוב להאכיל אותו בצורה מפוקחת ומסודרת. כלב צריך לקבל זמן קצוב של כרבע שעה בכל ארוחה. כל מה שהוא משאיר בצלחת צריך לקחת. הכלב צריך לאכול בזמנים קבועים. גור אוכל 3 פעמים ביום וכלב בוגר בין פעם אחת לפעמיים. הדרך היעילה ביותר לחינוך לצרכים, היא על ידי תיחום הגור במקום קטן המוגדר כ טריטוריה פנימית  (בעזרת גדר שמיועדת לכך) ולפזר לו אוכל לכלב בשטח הזה. כלב מטבעו ימנע ככל שיכול מעשיית צרכים במקום בו הוא אוכל, ישן ורובץ. התיחום חייב תמיד להתקשר עם משהו חיובי. לכן, לעולם לא להשתמש בו כעונש. אז מה עושים אם מצאנו שלולית שתן או חבילת הפתעה כשחזרתנו הביתה? לא עושים כלום. טעות נפוצה היא לכעוס ולהעניש את הגור על כך. הבעיה היא שהוא לא מבין ולא מקשר את העונש לצרכים. לכן, אסור לכעוס או להעניש, פשוט ללמוד להבא להוריד אותו מוקדם יותר. רצוי שלא לנקות את הצרכים מול הכלב בגלל שהוא עלול לחקות אתכם בעתיד ואף לאכול את צרכיו כדי להעלימם.
                    </p><p>
                    דברים נוספים שכדאי לקחת בחשבון
                    </p><p>
                    איך לבחור קולר לכלב? – קודם כל, חשוב לדאוג לקולר תקין שמתאים לגודל הכלב ולרצועת בד קצרה לטיולים. קולר לא בוחרים רק לפי צבע וגודל. כדאי לקנות קולר מתכוונן כשאורך רצועת הקולר צריך להיות כ-10 סמ יותר מהיקף הצוואר של הכלב. גורים קטנים יעדיפו רתמה המיועדת לגוף כולו. כלבים גדולים יעדיפו קולר בעל אחיזה טובה יותר.
                    </p><p>
                    פרטי קשר – מומלץ לדאוג לתג שם וטלפון ולהצמיד לקולר הכלב. כך שאם הכלב שלכם חלילה יילך לאיבוד, אדם אקראי שימצא אותו ברחוב, יוכל ליצור איתכם מיד קשר.
                    </p><p>
                    כלי למים ואוכל – חשוב לדאוג לכלים נקיים ומתאימים לגודל הכלב. אם הכלב שלכם גדול, אז כדאי גם לרכוש עם כליי המים והאוכל מתקן מיוחד שמגביה ומייצב את הצלחות של הכלב.
                    </p><p>
                    מרבץ נוח –  חשוב לבחור מקום נוח לשהייה היום יומית של הכלב שלכם. זה יכול להיות מיטה/שמיכה / שטיח / מזרון / כרית או כל מצע רך אחר שיהיה לכלב מקום נעים ונוח לרבוץ ולישון בו במהלך היום ובלילה.
                    </p><p>
                    מזון מתאים לכלב – חשוב לדאוג לתזונה נכונה ומתאימה לכלב. אם מדובר בגור קטן צריך להאכיל במזון מיוחד לגורים. אם מדובר בכלב מגזע גדול צריך להתאים את סוג המזון לגודל וגיל הכלב. בנושא הזה אתם מוזמנים להתייעץ איתנו והמומחים שלנו ישמחו להתאים את המזון המתאים עבור הכלב שלכם.
                    </p><p>
                    נלקח מ:
                    https://spca.co.il/%D7%9C%D7%90%D7%9E%D7%A5-%D7%9B%D7%9C%D7%91-%D7%97%D7%93%D7%A9-%D7%96%D7%95-%D7%94%D7%A0%D7%90%D7%94-%D7%90%D7%93%D7%99%D7%A8%D7%94-%D7%91%D7%95%D7%90%D7%95-%D7%9C%D7%9C%D7%9E%D7%95%D7%93-%D7%90/
                    </p>

                ",
                @"

                    <p>
                    הוכח כי בעלי חיים תורמים לבני האדם מבחינה בריאותית, רגשית וחברתית. מחקרים רבים מוכיחים את התועלת שבקשר של ילדים עם חיות מחמד
                    </p><p>
                    ילדים לומדים מהם הצרכים של חיית המחמד ומפתחים רגישות לצרכים של זולתם.
                    </p><p>
                    ילדים מטפלים בחיית המחמד ותוך כדי טיפול מפתחים תחושת אחריות ותחושת גאווה.
                    </p><p>
                    ילדים משחקים, מלטפים, מחבקים ומשוחחים עם חיית המחמד. קשר זה מפתח את יכולתם לתקשורת פיזית ומנטאלית עם העולם הסובב אותם.
                    </p><p>
                    הקשר לחיית המחמד מאפשר לילדים לבטא מגוון רחב של רגשות.
                    </p><p>
                    ילדים לומדים לתת מעצמם וליהנות מקבלת אהבה מחיית המחמד שלהם (יכולת קבלה ונתינה).
                    </p><p>
                    קשר לחיית המחמד מהווה גורם לחיזוק הביטחון העצמי. הילדים זוכים לתשומת לב מוחלטת מצד חיית המחמד שלהם, הם זוכים לחיבה, אהבה וחברות ללא תנאי.
                    </p><p>
                    ילדים שמטפלים בבעלי חיים, לומדים ומפתחים מיומנויות טיפול באחר ובשונה.
                    </p><p>
                    ילדים לומדים להבין מסרים שאינם מילוליים, ובכך מפתחים רגישות יתר והבנה כלפי זולתם.
                    </p><p>
                    ילדים שמטפחים חיית מחמד, יהיו יותר פתוחים וחברותיים ובדרך כלל ינהלו עם זולתם יחסים הדדים הוגנים.
                    </p><p>
                    גם המבוגרים נהנים ונתרמים מהקרבה לבעלי חיים. כלבים לדוגמא, תורמים לכושר הגופני ולפיתוח קשרים חברתיים חדשים (במהלך הטיפולים היומיומיים ובמסגרת השמירה על כושרו הפיזי של הכלב). מחקרים הוכיחו, שקרבת בני אדם לבעלי החיים וטיפול בהם תורמת להורדת לחץ דם ולרגיעה נפשית.
                    </p><p>

                    אצל קשישים, בעלי חיים מפיגים תחושת בדידות ומשמשים כחבר קרוב וחשוב. הם מעודדים את הקשיש לפעילות גופנית, וממלאים את הצורך שלו להרגיש חשוב ונחוץ. טיפול בבעלי חיים גורם להנאה ולסיפוק ללא לחצים ולבטים, המלווים קשר שיש בין בני אדם. למבוגרים, כמו לילדים, בעלי חיים מעניקים אהבה, קרבה וחברות ללא תנאי.
                    </p><p>
                    נלקח מ:
                    https://ks-loves-animals.co.il/%D7%9E%D7%93%D7%95%D7%A2-%D7%9B%D7%93%D7%90%D7%99-%D7%9C%D7%90%D7%9E%D7%A5-%D7%97%D7%99%D7%99%D7%AA-%D7%9E%D7%97%D7%9E%D7%93/
                    </p>
                ",

                @""

                //@"

                //    <p>
                //    ובכן, מדוע לאמץ כלב ולא לקנות כלב? שאלה טובה.זו שאלה, שנדמה לי כי אנשים שבאופן טבעי יפנו לאימוץ כלב כלל אינם שואלים אותה, ואנשים שבאופן טבעי יפנו לרכישת כלב – גם הם אינם שואלים אותה. אנסה, אם כן, להשיב עליה מתוך נקודת מבטי כשייכת לזן מאמצי הכלבים; תשובתי אולי תאיר את עיניהם של אלה שמעדיפים לקנות כלב, היא אולי תהפוך את המעשה הלא מודע של המאמצים הטבעיים – למעשה מודע, וחשוב מכל – אולי היא תוכל להבהיר למתלבטים מדוע כדאי לאין ערוך לאמץ כלב ולא לקנותו.
                //    </p><p>
                //    לאמץ כלב - להציל חיים
                //    </p><p>
                //    ראשית, אימוץ כלב הוא מעשה הומני, במובן הפשוט ביותר של המילה. זהו מעשה טוב לעשות; כל המאמץ כלב, מציל נפש אחת בעולם. ונוסף על כך שעשיתם מעשה טוב בעבור הכלב, גם הענקתם לעצמכם הרגשה טובה בבחירה שלכם, וביכולת ההשפעה שלכם על מידת עשיית הטוב בעולם.
                //    </p><p>
                //    שנית, אימוץ היא פעולה יעילה ו אקולוגית  בהרבה, פעולה סביבתית; אני מתכוונת לכך שהכלבים המיועדים לאימוץ, בין אם הם משוטטים ברחוב ובין אם הם כבר נמצאים תחת חסותה של עמותה המטפלת בהם – הם כלבים קיימים, שאין להם בית. כלבים רבים משוטטים כיום ברחובות, גורים עזובים, כלבים שננטשו או שברחו מבתים שבהם לא היה להם טוב, כלבים שהלכו לאיבוד ובעליהם לא חיפשו אותם או שלא מצאו אותם... הם נמצאים והם מחכים לבית אוהב כפי שהם מכירים וכפי שמגיע להם. הכלבים הללו יהיו אסירי תודה לבעליהם החדשים, ואתם – הבעלים החדשים, תזכו בקשר אוהב ומוקיר למשך שארית חייו של הכלב.
                //    </p><p>
                //    נקודה נוספת היא העובדה כי תהליך האימוץ זול בהרבה מתהליך קנייה של כלב, ובנוסף – כלבים שניתן לקנות הם ברוב המקרים כלבים גזעיים. מבלי להיכנס לבעייתיות הנגרמת לעתים כתוצאה מתהליכי "השבחת הגזע" שהם עוברים, פשוט מדובר בהוצאה גדולה ובכסף רב שמשלמים בעבור תעודות, בעבור "שייכות לגזע", וכדומה. בסופו של יום, כלב הוא עוד יצור חי בבית, ותשאלו כל בעל כלב – זה כלל לא משנה מאיזה גזע הוא וזה אפילו לא משנה איך הוא נראה; הוא שלכם, וכאשר משקיעים במישהו ומטפלים בו – אוהבים אותו, והוא בתמורה משיב לכם באהבה, בנאמנות ובקשר הייחודי הנוצר בין כלב ובין בעליו.
                //    </p>< p>
                //    אימוץ כלבים - להעניק לכלבים בית
                //    </p><p>
                //    לסיכום, הטיעונים אולי אינם רבים אך בעיניי הם בהחלט מכריעים; אימוץ כלב מונע שיטוט של כלבים ברחובות, הוא מונע מוות מיותר של יצורים חיים, והוא "חסכוני" במובן העמוק ביותר של המילה – הוא פשוט דואג למי שכבר קיים. וזאת במקום שמוכרי כלבים ידאגו ל"ייצור" של עוד ועוד גורים חדשים שאותם יוכלו למכור תמורת כסף.
                //    </p><p>
                //    בנוסף, זכרו כי גם כלב שאינו משוטט ברחוב אלא נמצא בטיפול של עמותת מחסה אינו יכול להישאר שם לנצח, וזה מעבר לעובדה הפשוטה ששם – עם כל המסירות, הטיפול והאהבה שהוא מקבל – עדיין חייו אינם דומים כהוא זה לחיים שיזכה להם בבית משלו, תחת טיפולם של אדם או של משפחה שיאהבו אותו ויקדישו לו תשומת לב ושאצלם הוא יהיה חלק אינטגרלי מהבית.כלב זקוק לחברה, הוא זקוק לתשומת לב והוא זקוק ליחסי גומלין, לאינטראקציה עם בני אדם.
                //    </p><p>
                //    אז מה אתם אומרים? לקנות או לאמץ? נדמה לי שהתשובה ברורה בהרבה כעת...
                //    </p><p>
                //     כתבה וערכה: הילה שביט
                //     נלקח מ:
                //    https://www.yad4.co.il/article-67
                //    </ p >

                //",
            };



            for (int i = 1; i <= articleTitles.Length; i++)
            {
                var mtf = context.MetaFileLinks.Add(new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = articleImages[i - 1], CreationTimestamp = DateTime.Now, UpdatedTimestamp = DateTime.Now });

                await context.SaveChangesAsync();

                context.Articles.Add(new Article
                {
                    Title = articleTitles[i - 1],
                    Content = articleContent[i - 1],
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