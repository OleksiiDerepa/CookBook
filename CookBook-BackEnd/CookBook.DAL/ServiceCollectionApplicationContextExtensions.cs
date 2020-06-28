using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.DAL
{
    public static class ServiceCollectionApplicationContextExtensions
    {
        public static IServiceCollection AddApplicationDataStores(
           this IServiceCollection services, 
            IConfiguration configuration)
        {
            
            string connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                connection, x => x.CommandTimeout(10000)));

            return services;
        }
    }
}