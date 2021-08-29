﻿// <auto-generated />
using System;
using GetPet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GetPet.Data.Migrations
{
    [DbContext(typeof(GetPetDbContext))]
    [Migration("20210723084701_IsBoolean")]
    partial class IsBoolean
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("GetPet.Data.Entities.AnimalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AnimalTypes");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("MetaFileLinkId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MetaFileLinkId")
                        .IsUnique()
                        .HasFilter("[MetaFileLinkId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("GetPet.Data.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("GetPet.Data.Entities.DataSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("CronCrawlTime")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Name")
                        .HasMaxLength(400)
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("Url")
                        .HasMaxLength(1000)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DataSources");
                });

            modelBuilder.Entity("GetPet.Data.Entities.DataSourceLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("DataSourceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LogText")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DataSourceId");

                    b.ToTable("DataSourceLogs");
                });

            modelBuilder.Entity("GetPet.Data.Entities.EmailHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EmailHistories");
                });

            modelBuilder.Entity("GetPet.Data.Entities.MetaFileLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MimeType")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Path")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int?>("PetId")
                        .HasColumnType("int");

                    b.Property<decimal>("Size")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("MetaFileLinks");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("GetPet.Data.Entities.NotificationTrait", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("NotificationId")
                        .HasColumnType("int");

                    b.Property<int>("TraitId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.HasIndex("TraitId");

                    b.ToTable("NotificationTraits");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("Source")
                        .HasColumnType("int");

                    b.Property<string>("SourceLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("GetPet.Data.Entities.PetHistoryStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("PetHistoryStatuses");
                });

            modelBuilder.Entity("GetPet.Data.Entities.PetTrait", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<int>("TraitId")
                        .HasColumnType("int");

                    b.Property<int?>("TraitOptionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.HasIndex("TraitId");

                    b.HasIndex("TraitOptionId");

                    b.ToTable("PetTraits");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Trait", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("FemaleName")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<bool>("IsBoolean")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("TraitType")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Traits");
                });

            modelBuilder.Entity("GetPet.Data.Entities.TraitOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("FemaleOption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Option")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TraitId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TraitId");

                    b.ToTable("TraitOptions");
                });

            modelBuilder.Entity("GetPet.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<bool>("EmailSubscription")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int?>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Article", b =>
                {
                    b.HasOne("GetPet.Data.Entities.MetaFileLink", "MetaFileLink")
                        .WithOne()
                        .HasForeignKey("GetPet.Data.Entities.Article", "MetaFileLinkId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GetPet.Data.Entities.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MetaFileLink");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Comment", b =>
                {
                    b.HasOne("GetPet.Data.Entities.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GetPet.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GetPet.Data.Entities.DataSourceLog", b =>
                {
                    b.HasOne("GetPet.Data.Entities.DataSource", "DataSource")
                        .WithMany()
                        .HasForeignKey("DataSourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DataSource");
                });

            modelBuilder.Entity("GetPet.Data.Entities.EmailHistory", b =>
                {
                    b.HasOne("GetPet.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GetPet.Data.Entities.MetaFileLink", b =>
                {
                    b.HasOne("GetPet.Data.Entities.Pet", null)
                        .WithMany("MetaFileLinks")
                        .HasForeignKey("PetId");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Notification", b =>
                {
                    b.HasOne("GetPet.Data.Entities.AnimalType", "AnimalType")
                        .WithMany()
                        .HasForeignKey("AnimalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GetPet.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GetPet.Data.Entities.NotificationTrait", b =>
                {
                    b.HasOne("GetPet.Data.Entities.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GetPet.Data.Entities.Trait", "Trait")
                        .WithMany()
                        .HasForeignKey("TraitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notification");

                    b.Navigation("Trait");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Pet", b =>
                {
                    b.HasOne("GetPet.Data.Entities.AnimalType", "AnimalType")
                        .WithMany()
                        .HasForeignKey("AnimalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GetPet.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GetPet.Data.Entities.PetHistoryStatus", b =>
                {
                    b.HasOne("GetPet.Data.Entities.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("GetPet.Data.Entities.PetTrait", b =>
                {
                    b.HasOne("GetPet.Data.Entities.Pet", "Pet")
                        .WithMany("PetTraits")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GetPet.Data.Entities.Trait", "Trait")
                        .WithMany()
                        .HasForeignKey("TraitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GetPet.Data.Entities.TraitOption", "TraitOption")
                        .WithMany()
                        .HasForeignKey("TraitOptionId");

                    b.Navigation("Pet");

                    b.Navigation("Trait");

                    b.Navigation("TraitOption");
                });

            modelBuilder.Entity("GetPet.Data.Entities.TraitOption", b =>
                {
                    b.HasOne("GetPet.Data.Entities.Trait", "Trait")
                        .WithMany("TraitOptions")
                        .HasForeignKey("TraitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trait");
                });

            modelBuilder.Entity("GetPet.Data.Entities.User", b =>
                {
                    b.HasOne("GetPet.Data.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GetPet.Data.Entities.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.Navigation("City");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Article", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Pet", b =>
                {
                    b.Navigation("MetaFileLinks");

                    b.Navigation("PetTraits");
                });

            modelBuilder.Entity("GetPet.Data.Entities.Trait", b =>
                {
                    b.Navigation("TraitOptions");
                });

            modelBuilder.Entity("GetPet.Data.Entities.User", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
