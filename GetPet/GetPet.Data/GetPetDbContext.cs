using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GetPet.Data
{
    public class GetPetDbContext : DbContext
    {
        #region Ctor

        public GetPetDbContext() : base() { }

        public GetPetDbContext(DbContextOptions<GetPetDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory()).FullName}\\GetPet.WebApi")
                   .AddJsonFile("appsettings.json")
                   .Build();

                var connectionString = configuration.GetConnectionString("GetPetConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        } 

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region PetTrait

            modelBuilder.Entity<PetTrait>()
                .HasOne<Pet>(pt => pt.Pet);

            modelBuilder.Entity<PetTrait>()
                .HasOne<Trait>(pt => pt.Trait);

            #endregion

            #region DataSourceLog

            modelBuilder.Entity<DataSourceLog>()
                .HasOne<DataSource>(dsl => dsl.DataSource);

            #endregion

            #region EmailHistory

            modelBuilder.Entity<EmailHistory>()
                .HasOne<User>(eh => eh.User);


            #endregion

            #region MetaFileLink

            modelBuilder.Entity<MetaFileLink>()
                .HasOne<Pet>(mfl => mfl.Pet);

            #endregion

            #region Notification

            modelBuilder.Entity<Notification>()
                .HasOne<User>(n => n.User);

            modelBuilder.Entity<Notification>()
                .HasOne<AnimalType>(n => n.AnimalType);

            #endregion

            #region NotificationTrait
            
            modelBuilder.Entity<NotificationTrait>()
                .HasOne<Notification>(nt => nt.Notification);

            modelBuilder.Entity<NotificationTrait>()
                .HasOne<Trait>(nt => nt.Trait);

            #endregion

            #region Pets                       

            modelBuilder.Entity<Pet>()
                .HasOne<AnimalType>(p => p.AnimalType);

            modelBuilder.Entity<Pet>()
                .HasOne<User>(p => p.User);

            #endregion

            #region PetHistoryStatus

            modelBuilder.Entity<PetHistoryStatus>()
                .HasOne<Pet>(phs => phs.Pet);

            #endregion

            #region PetTrait
            
            modelBuilder.Entity<PetTrait>()
                .HasOne<Pet>(pt => pt.Pet);

            modelBuilder.Entity<PetTrait>()
                .HasOne<Trait>(pt => pt.Trait);

            #endregion

            #region Users

            modelBuilder.Entity<User>()
                .HasOne<City>(u => u.City);

            modelBuilder.Entity<User>()               
                .HasOne<Organization>(u => u.Organization);

            #endregion

        }

        public DbSet<AnimalTrait> AnimalTraits { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<DataSourceLog> DataSourceLogs { get; set; }
        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<EmailHistory> EmailHistories { get; set; }
        public DbSet<MetaFileLink> MetaFileLinks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationTrait> NotificationTraits { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetHistoryStatus> PetHistoryStatuses { get; set; }
        public DbSet<PetTrait> PetTraits { get; set; }
        public DbSet<Trait> Traits { get; set; }
        public DbSet<User> Users { get; set; }
    }
}