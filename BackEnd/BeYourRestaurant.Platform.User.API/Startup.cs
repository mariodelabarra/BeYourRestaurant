using BeYourRestaurant.Platform.Core.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeYourRestaurant.Platform.User.API
{
    public class Startup : BaseApiStartup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) : base(configuration, webHostEnvironment)
        {
            RegisterValidatorFromAssemblyContaining<Domain.User>();
        }

        public override void ConfigureDepencencies(IServiceCollection services)
        {
            DependencyInjection.ConfigureDependencies(services, Configuration);
        }
    }
}
