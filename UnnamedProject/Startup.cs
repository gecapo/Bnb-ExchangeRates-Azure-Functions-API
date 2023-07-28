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
            builder.Services.AddAutoMapper(typeof(MapProfile));
            builder.Services.AddTransient(typeof(IBnbClient), typeof(BnbClient));
            //var secretClient = new SecretClient(keyValutUrl, new DefaultAzureCredential());
            //var apiKey = secretClient.GetSecret("").Value.Value;
        }
    }
}

