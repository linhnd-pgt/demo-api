using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Config;
using Service.Helpers;
using Service.Repositories.Base;
using Service.Services;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;

namespace Service
{

    public static class ServiceExtensions
    {

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RepositoryContext>(
                options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 34))));


            // CLoudinary set up
            var cloudinarySettings = new CloudinaryConfig
            {
                CloudName = "duylinhmedia",
                ApiKey = "944914366153752",
                ApiSecret = "Qv6TlyPXA0rqWzsWzPlDkHzxMcs"
            };

            configuration.GetSection("Cloudinary").Bind(cloudinarySettings);

            var account = new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret);

            var cloudinary = new Cloudinary("cloudinary://944914366153752:Qv6TlyPXA0rqWzsWzPlDkHzxMcs@duylinhmedia");

            services.AddSingleton(cloudinary);

        }

        public static void ConfigureCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
/*            services.AddScoped<IAuthorMapper, AuthorMapper>();*/

            //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            
        }

    }
}
