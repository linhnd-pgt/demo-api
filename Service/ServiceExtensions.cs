using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Repositories.Base;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{

    public static class ServiceExtensions
    {

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RepositoryContext>(
                options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 34))));


        }

        public static void ConfigureCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        }

    }
}
