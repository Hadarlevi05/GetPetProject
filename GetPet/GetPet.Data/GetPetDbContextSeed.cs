using GetPet.Common;
using GetPet.Data.Entities;
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
                context.Cities.Add(new City { Name = city, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
            }

            await context.SaveChangesAsync();

            var telAviv = context.Cities.SingleOrDefault(c => c.Name.Contains("תל אביב"));

            await context.SaveChangesAsync();

            var animalTypeList = new[] {
                "כלבים",
                "חתולים",
                "מכרסמים"
            };

            foreach (var item in animalTypeList)
            {
                context.AnimalTypes.Add(new AnimalType { Name = item, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
                await context.SaveChangesAsync();
            }

            var dog = context.AnimalTypes.FirstOrDefault();
            var cat = context.AnimalTypes.Skip(1).Take(1).FirstOrDefault();

            await context.SaveChangesAsync();

            // Dogs
            var size = context.Traits.Add(new Trait { Name = "גודל", FemaleName = "גודל", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow, TraitType = Enums.TraitType.Values });
            var color = context.Traits.Add(new Trait { Name = "צבע", FemaleName = "צבע", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow, TraitType = Enums.TraitType.Values });
            var goodWithKids = context.Traits.Add(new Trait { Name = "מסתדר עם ילדים", FemaleName = "מסתדרת עם ילדים", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow, TraitType = Enums.TraitType.Boolean });
            var goodWithPpl = context.Traits.Add(new Trait { Name = "מסתדר עם אנשים", FemaleName = "מסתדרת עם אנשים", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow, TraitType = Enums.TraitType.Boolean });
            var goodWithDogs = context.Traits.Add(new Trait { Name = "מסתדר עם כלבים", FemaleName = "מסתדרת עם כלבים", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow, TraitType = Enums.TraitType.Boolean });
            var trained = context.Traits.Add(new Trait { Name = "מאולף", FemaleName = "מאולפת", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow, TraitType = Enums.TraitType.Boolean });
            var worried = context.Traits.Add(new Trait { Name = "חששן", FemaleName = "חששנית", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow, TraitType = Enums.TraitType.Boolean });
            var vaccinated = context.Traits.Add(new Trait { Name = "מחוסן", FemaleName = "מחוסנת", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow, TraitType = Enums.TraitType.Boolean });
            var mixed = context.Traits.Add(new Trait { Name = "מעורב", FemaleName = "מעורבת", AnimalTypeId = dog.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow, TraitType = Enums.TraitType.Boolean });

            // Cats
            var colorCat = context.Traits.Add(new Trait { Name = "צבע", FemaleName = "צבע", AnimalTypeId = cat.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow, TraitType = Enums.TraitType.Values });

            await context.SaveChangesAsync();

            var traitOptionSmall = context.TraitOptions.Add(new TraitOption { Option = "קטן", FemaleOption= "קטנה", TraitId = size.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "בינוני", FemaleOption = "בינונית", TraitId = size.Entity.Id });
            var traitOptionBig = context.TraitOptions.Add(new TraitOption { Option = "גדול", FemaleOption = "גדול", TraitId = size.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "ענק", FemaleOption = "ענקית", TraitId = size.Entity.Id });

            context.TraitOptions.Add(new TraitOption { Option = "לבן", FemaleOption = "לבנה", TraitId = color.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "שחור", FemaleOption = "שחורה", TraitId = color.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "בז'", FemaleOption = "בז'", TraitId = color.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "דלמטי", FemaleOption = "דלמטית", TraitId = color.Entity.Id });

            AddBooleanTraitOption(context, goodWithKids);
            AddBooleanTraitOption(context, trained);
            AddBooleanTraitOption(context, goodWithPpl);
            AddBooleanTraitOption(context, goodWithDogs);
            AddBooleanTraitOption(context, worried);
            AddBooleanTraitOption(context, vaccinated);
            AddBooleanTraitOption(context, mixed);

            await context.SaveChangesAsync();

            var system = context.Users.Add(new User
            {
                CityId = telAviv.Id,
                Name = "מערכת",
                Email = "support@getpet.co.il",
                UserType = Enums.UserType.Admin,
                EmailSubscription = true,
                PasswordHash = SecurePasswordHasher.Hash("123456"),
                CreationTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow
            });

            var hadar = context.Users.Add(new User
            {
                CityId = telAviv.Id,
                Name = "הדר",
                Email = "hadar@getpet.co.il",
                UserType = Enums.UserType.Admin,
                EmailSubscription = true,
                PasswordHash = SecurePasswordHasher.Hash("123456"),
                CreationTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow
            });

            var sharon = context.Users.Add(new User
            {
                CityId = telAviv.Id,
                Name = "שרון",
                Email = "sharon@getpet.co.il",
                UserType = Enums.UserType.Admin,
                EmailSubscription = true,
                PasswordHash = SecurePasswordHasher.Hash("123456"),
                CreationTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow
            });

            var liza = context.Users.Add(new User
            {
                CityId = telAviv.Id,
                Name = "ליזה",
                Email = "liza@getpet.co.il",
                UserType = Enums.UserType.Admin,
                EmailSubscription = true,
                PasswordHash = SecurePasswordHasher.Hash("123456"),
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
                "לואי שלנו הוא כלב חברותי ואהוב על כל הבריות, אוהב לשחק מאוד ונהדר עם ילדים נשים וטף",
                "כלב חתיך ויפיוף",
                "לואי לואי לואי לורם יפסום"
            };

            for (int i = 0; i < names.Length; i++)
            {
                context.Pets.Add(new Pet
                {
                    Name = names[i],
                    AnimalTypeId = dog.Id,
                    Birthday = DateTime.UtcNow.AddYears(-2),
                    Gender = Enums.Gender.Male,
                    Status = Enums.PetStatus.WaitingForAdoption,
                    UserId = system.Entity.Id,
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

                pet.PetTraits.Add(new PetTrait { PetId = pet.Id, Trait = size.Entity, TraitOptionId = traitOptionBig.Entity.Id, CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
                pet.PetTraits.Add(new PetTrait { PetId = pet.Id, Trait = color.Entity, Description = "בז'", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });

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
                var mtf = context.MetaFileLinks.Add(new MetaFileLink { MimeType = "image/jpeg", Size = 1000, Path = $"{Constants.WEBAPI_URL}images/mocks/article{i}.jpg", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });

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
                            Text ="כתבה מאוד מועילה",
                            UserId = liza.Entity.Id,
                        },
                        new Comment {
                            Text ="טוב לדעת",
                            UserId = sharon.Entity.Id,
                        }
                    }
                });
            }
            await context.SaveChangesAsync();

            await transaction.CommitAsync();
        }

        private static void AddBooleanTraitOption(GetPetDbContext context, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Trait> vaccinated)
        {
            context.TraitOptions.Add(new TraitOption { Option = "כן", FemaleOption = "כן", TraitId = vaccinated.Entity.Id });
            context.TraitOptions.Add(new TraitOption { Option = "לא", FemaleOption = "לא", TraitId = vaccinated.Entity.Id });
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