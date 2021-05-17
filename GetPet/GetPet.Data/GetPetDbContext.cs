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

            modelBuilder.Entity<Pet>()
                .HasMany<PetTrait>(p => p.PetTraits);

            #endregion

            #region PetHistoryStatus

            modelBuilder.Entity<PetHistoryStatus>()
                .HasOne<Pet>(phs => phs.Pet);

            #endregion

            #region PetTrait

            modelBuilder.Entity<PetTrait>()
                .HasOne<Pet>(pt => pt.Pet)
                .WithMany(p => p.PetTraits)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PetTrait>()
                .HasOne<Trait>(pt => pt.Trait);                     

            #endregion

            #region Trait

            modelBuilder.Entity<Trait>()
                .HasMany<TraitOption>(pt => pt.TraitOptions)
                .WithOne(to => to.Trait)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region TraitOptions

            modelBuilder.Entity<TraitOption>()
                .HasOne<Trait>(to => to.Trait)
                .WithMany(t => t.TraitOptions)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Users

            modelBuilder.Entity<User>()
                .HasOne<City>(u => u.City);

            modelBuilder.Entity<User>()
                .HasOne<Organization>(u => u.Organization);

            #endregion

            #region Articles

            modelBuilder.Entity<Article>()
                .HasOne<User>(a => a.User)
                .WithMany(u => u.Articles)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Article>()
                .HasOne<MetaFileLink>(a => a.MetaFileLink)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Article>()
                 .HasMany<Comment>(a => a.Comments)
                 .WithOne(c => c.Article)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne<User>(a => a.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

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
        public DbSet<TraitOption> TraitOptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}