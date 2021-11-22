using BeYourRestaurant.Platform.Core.Postgres;
using BeYourRestaurant.Platform.Core.Postgres.Repository;
using BeYourRestaurant.Platform.Core.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace BeYourRestaurant.Platform.User.API
{
    public static class DependencyInjection
    {
        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterConfigutarion(services, configuration);
            RegisterServices(services);
            RegisterRepositories(services);
        }

        public static void RegisterConfigutarion(IServiceCollection services, IConfiguration configuration)
        {
            //Connection to Postgres
            services.AddSingleton<DapperDbContext>();
        }

        public static void RegisterServices(IServiceCollection services)
        {
            //Mapper

            //Services

            //Validators
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            //BaseRepositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //Repositories
        }
    }
}
