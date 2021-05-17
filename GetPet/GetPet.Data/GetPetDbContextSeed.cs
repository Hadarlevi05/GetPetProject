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

                pet.PetTraits.Add(new PetTrait { PetId = pet.Id, Trait = size.Entity, Description = "גדול", CreationTimestamp = DateTime.UtcNow, UpdatedTimestamp = DateTime.UtcNow });
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