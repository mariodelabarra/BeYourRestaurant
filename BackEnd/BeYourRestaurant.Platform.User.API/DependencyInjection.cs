﻿using BeYourRestaurant.Platform.Core.API;
using BeYourRestaurant.Platform.Core.Postgres;
using BeYourRestaurant.Platform.Core.Postgres.Repository;
using BeYourRestaurant.Platform.Core.Repository;
using BeYourRestaurant.Platform.User.Repository;
using BeYourRestaurant.Platform.User.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            //Connection to PostgresSQL
            services.AddScoped(x => new PostgressDbSession(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //LoggerManager
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void RegisterServices(IServiceCollection services)
        {
            //Mapper

            //Services
            services.AddScoped<IUserService, UserService>();

            //Validators
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            //BaseRepositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
