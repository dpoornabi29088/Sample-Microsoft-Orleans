using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Net;
using Test.Grains;
using Test.IGrains.Interfaces;

var host = new HostBuilder()
    .UseOrleans((context, siloBuilder) =>
    {
        siloBuilder.Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "my-first-cluster";
            options.ServiceId = "my-orleans-service";
        });

        siloBuilder.UseLocalhostClustering();
        siloBuilder.Configure<EndpointOptions>(options =>
        {
            options.AdvertisedIPAddress = IPAddress.Loopback;
            options.SiloPort = 11111;
            options.GatewayPort = 30000;
        });

        //
        //siloBuilder.ConfigureServices(services => { services.AddSingleton<ITestGrain, TestGrain>(); });

    })
    .ConfigureServices(services =>
    {
    })
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();
