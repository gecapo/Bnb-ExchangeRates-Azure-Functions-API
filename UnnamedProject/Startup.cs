using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(UnnamedProject.Startup))]

namespace UnnamedProject
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var keyValutUrl = new Uri(Environment.GetEnvironmentVariable("KeyValutUrl"));
            //var secretClient = new SecretClient(keyValutUrl, new DefaultAzureCredential());
            //var apiKey = secretClient.GetSecret("").Value.Value;

            builder.Services.AddAutoMapper(typeof(MapProfile));
            builder.Services.AddScoped(typeof(IBnbClientService), typeof(BnbClient));
            builder.Services.AddScoped(typeof(ITableStorageService), typeof(TableStorageService));
        }
    }
}

