using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.MappingProfiles;
using GetPet.BusinessLogic.Repositories;
using GetPet.Common;
using GetPet.Crawler.Crawlers;
using GetPet.Crawler.Crawlers.Abstractions;
using GetPet.Data;
using GetPet.Scheduler.Jobs;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace GetPet.Scheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            SetEnvironmentVariables();
        }

        private void SetEnvironmentVariables()
        {
            Constants.WEBAPI_URL = Configuration.GetValue<string>("WebApiUrl");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string sqlConnectionString = Configuration.GetConnectionString("HangfireConnection");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GetPet.Scheduler", Version = "v1" });
            });

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(sqlConnectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true                                        
                }));

            var sqlStorage = new SqlServerStorage(sqlConnectionString);
            var options = new BackgroundJobServerOptions
            {
                ServerName = "GetPet HangFire Server"
            };
            JobStorage.Current = sqlStorage;

            // Add the processing server as IHostedService
            services.AddHangfireServer();
           
            var serviceProvider = services
                .AddDbContext<GetPetDbContext>(options =>
                {
                    options.UseSqlServer(sqlConnectionString);
                    options.EnableSensitiveDataLogging();
                })
                .AddAutoMapper(typeof(GetPetProfile))
                .AddScoped<IPetRepository, PetRepository>()
                .AddScoped<IGetPetDbContextSeed, GetPetDbContextSeed>()
                .AddScoped<ITraitRepository, TraitRepository>()
                .AddScoped<ICityRepository, CityRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAnimalTypeRepository, AnimalTypeRepository>()
                .AddScoped<IPetHandler, PetHandler>()
                .AddScoped<RehovotSpaCrawler, RehovotSpaCrawler>()
                .AddScoped<SpcaCrawler, SpcaCrawler>()
                .AddScoped<IUserHandler, UserHandler>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IEmailHistoryRepository, EmailHistoryRepository>()
                .AddScoped<INotificationRepository, NotificationRepository>()
                .AddTransient<IMailHandler, MailHandler>();

            SetBackgroundJobs();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GetPet.Scheduler v1"));
            }

            app.UseHttpsRedirection();

            app.UseHangfireDashboard();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }

        public void SetBackgroundJobs()
        {
            RecurringJob.AddOrUpdate<RehovotSpaJob>("RehovotSpaJob", job => job.Execute(), Cron.Minutely);
            RecurringJob.AddOrUpdate<SpcaJob>("SpcaJob", job => job.Execute(), Cron.Minutely);
            RecurringJob.AddOrUpdate<NotificationSenderJob>("NotificationSenderJob", job => job.Execute(), Cron.Minutely);
        }
    }
}