using GetPet.Common;
using GetPet.Data.Entities;
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

            var city1 = context.Cities.Add(new City { Name = "תל אביב" });
            var city2 = context.Cities.Add(new City { Name = "כפר-סבא" });

            await context.SaveChangesAsync();

            var dog = context.AnimalTypes.Add(new AnimalType { Name = "כלב" });
            var cat = context.AnimalTypes.Add(new AnimalType { Name = "חתול" });
            context.AnimalTypes.Add(new AnimalType { Name = "שפן" });

            await context.SaveChangesAsync();

            var size = context.Traits.Add(new Trait { Name = "גודל" });
            var color = context.Traits.Add(new Trait { Name = "צבע" });
            context.Traits.Add(new Trait { Name = "מסתדר עם ילדים" });
            context.Traits.Add(new Trait { Name = "מאולף" });

            await context.SaveChangesAsync();

            context.AnimalTraits.Add(new AnimalTrait { Trait = size.Entity, AnimalType = dog.Entity });
            context.AnimalTraits.Add(new AnimalTrait { Trait = size.Entity, AnimalType = cat.Entity });

            await context.SaveChangesAsync();

            PasswordHashHelper hash = new PasswordHashHelper("password");
            var hashPassword = System.Text.Encoding.Default.GetString(hash.Hash);

            context.Users.Add(new User
            {
                City = city1.Entity,
                Name = "הדר",
                Email = "hadar@getpet.co.il",
                UserType = Enums.UserType.Regular,
                EmailSubscription = true,
                PasswordHash = hashPassword
            });

            await context.SaveChangesAsync();
        }
    }


}
