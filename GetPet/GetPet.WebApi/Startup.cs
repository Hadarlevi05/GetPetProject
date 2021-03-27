using GetPet.BusinessLogic.MappingProfiles;
using GetPet.BusinessLogic.Repositories;
using GetPet.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PetAdoption.BusinessLogic.Repositories;

namespace GetPet.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            string sqlConnectionString = Configuration.GetConnectionString("GetPetConnectionString");

            services.AddDbContext<GetPetDbContext>(
                options => options.UseSqlServer(sqlConnectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GetPet.WebApi", Version = "v1" });
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddAutoMapper(typeof(GetPetProfile));

            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGetPetDbContextSeed, GetPetDbContextSeed>();


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


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            getPetDbContext.Database.Migrate();
            getPetDbContextSeed.Seed();
        }
    }
}