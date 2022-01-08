using BeYourRestaurant.Platform.Core.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeYourRestaurant.Platform.User.API
{
    public class Startup : BaseApiStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureDepencencies(IServiceCollection services)
        {
            DependencyInjection.ConfigureDependencies(services, Configuration);
        }
    }
}
