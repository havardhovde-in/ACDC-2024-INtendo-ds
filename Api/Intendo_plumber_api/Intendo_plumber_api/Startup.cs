using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Intendo_plumber_api.Startup))]

namespace Intendo_plumber_api
{

  public class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      var context = builder.GetContext();

      builder.Services.AddOptions<Config>()
              .Configure<IConfiguration>((settings, configuration) =>
              {
                configuration.GetSection("Config").Bind(settings);
              });
    }
  }
}