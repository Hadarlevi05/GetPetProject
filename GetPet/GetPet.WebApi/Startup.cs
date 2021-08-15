using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.MappingProfiles;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.BusinessLogic.Handlers;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.Common;
using GetPet.Data;
using GetPet.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GetPet.WebApi
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
            Constants.Secret = Configuration.GetValue<string>("Secret");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string sqlConnectionString = Configuration.GetConnectionString("GetPetConnectionString");

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddDbContext<GetPetDbContext>(options =>
                {
                    options.UseSqlServer(sqlConnectionString);
                    options.EnableSensitiveDataLogging();
                })

                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GetPet.WebApi", Version = "v1" });
                })
                .AddRouting(options => options.LowercaseUrls = true)

                .AddCors(o => o.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }))
                .AddAutoMapper(typeof(GetPetProfile))
                .AddScoped<IPetRepository, PetRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ICityRepository, CityRepository>()
                .AddScoped<IAnimalTypeRepository, AnimalTypeRepository>()
                .AddScoped<ITraitRepository, TraitRepository>()
                .AddScoped<IOrganizationRepository, OrganizationRepository>()
                .AddScoped<IPetTraitRepository, PetTraitRepository>()
                .AddScoped<ITraitOptionRepository, TraitOptionRepository>()
                .AddScoped<IArticleRepository, ArticleRepository>()
                .AddScoped<ICommentRepository, CommentRepository>()
                .AddScoped<IMetaFileLinkRepository, MetaFileLinkRepository>()
                .AddScoped<IGetPetDbContextSeed, GetPetDbContextSeed>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<INotificationHandler, NotificationHandler>()
                .AddScoped<INotificationRepository, NotificationRepository>()
                .AddScoped<IMailHandler, MailHandler>()
                .AddScoped<IUserHandler, UserHandler>()
                .AddScoped<IEmailHistoryRepository, EmailHistoryRepository>()
                .AddScoped(sp => Configuration.GetSection("MailSettings").Get<MailSettings>())
                .AddScoped<IEmailHistoryRepository, EmailHistoryRepository>()
                .AddScoped<IPetHandler, PetHandler>()
                .AddScoped<IPetHistoryStatusRepository, PetHistoryStatusRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GetPetDbContext getPetDbContext, IGetPetDbContextSeed getPetDbContextSeed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GetPet.WebApi v1"));
            }
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            getPetDbContext.Database.Migrate();
            getPetDbContextSeed.Seed();
        }
    }
}