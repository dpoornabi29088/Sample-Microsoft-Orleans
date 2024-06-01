using Orleans.Configuration;
using Test.Grains;
using Test.IGrains.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the Orleans client
builder.Host.UseOrleansClient(clientBuilder =>
{
    clientBuilder.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "my-first-cluster";
        options.ServiceId = "my-orleans-service";
    });

    clientBuilder.UseLocalhostClustering();
    //clientBuilder.ConfigureServices(services => { services.AddSingleton<ITestGrain, TestGrain>(); });

});

//builder.Services.AddSingleton<IClusterClient>(sp => sp.GetService<IClusterClient>());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapGet("/Get", (int value, IGrainFactory grainFactory) =>
{
    var testGrain = grainFactory.GetGrain<ITestGrain>("Test");
    testGrain.AddInstruction(value);

});

app.MapGet("/Add", (int value, IGrainFactory grainFactory) =>
{
    var testGrain = grainFactory.GetGrain<ITestGrain>("Test");
    testGrain.AddInstruction(value);

});

app.Run();
