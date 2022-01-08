using FluentValidation;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace BeYourRestaurant.Platform.Core.API
{
    public abstract class BaseApiStartup
    {
        public IConfiguration Configuration { get; }
        private List<Type> AssemblyTypes { get; }

        public BaseApiStartup(IConfiguration configuration)
        {
            Configuration = configuration;
            AssemblyTypes = new List<Type>();
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            RegisterValidatorFromAssemblyContaining<BaseApiStartup>();
        }

        /// <summary>
        /// To be overriten for any API Service that inherit from this base class
        /// E.g: For dependency injection for any project
        /// </summary>
        /// <param name="services"></param>
        public abstract void ConfigureDepencencies(IServiceCollection services);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation(fv => 
                {
                    fv.ImplicitlyValidateChildProperties = true;
                    fv.ImplicitlyValidateRootCollectionElements = true;
                    RegisterValidatorsInControllers(fv);
                })
                .AddProblemDetailsConventions()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.AddDefaultOptions();
                });

            services.AddProblemDetails();

            ConfigureCorsPolicy(services);
            AddValidatorDependencies(services);
            ConfigureDepencencies(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BeYourRestaurant.Platform.User.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            //To transforming error status codes to ProblemDetails
            app.UseProblemDetails();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeYourRestaurant.Platform.User.API v1"));
            }

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureCorsPolicy(IServiceCollection services)
        {
            services.AddCors(opt => opt.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        }

        /// <summary>
        /// Register all validators derived from AbstractValidator within the assembly containing
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        protected void RegisterValidatorFromAssemblyContaining<TEntity>()
        {
            AssemblyTypes.Add(typeof(TEntity));
        }

        /// <summary>
        /// Registers all validators derived from AbstractValidators within the assembly containing the specified type
        /// </summary>
        private void RegisterValidatorsInControllers(FluentValidationMvcConfiguration fv)
        {
            foreach(var type in AssemblyTypes)
            {
                fv.RegisterValidatorsFromAssemblyContaining(type);
            }
        }

        /// <summary>
        /// Adds all validators in the assembly of the specified type
        /// </summary>
        /// <param name="services"></param>
        private void AddValidatorDependencies(IServiceCollection services)
        {
            foreach(var type in AssemblyTypes)
            {
                services.AddValidatorsFromAssemblyContaining(type);
            }
        }
    }
}
