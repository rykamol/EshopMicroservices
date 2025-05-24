using Ordering.Application;
using Ordering.Api;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();




var app = builder.Build();


//Configure the Http request pipeline
app.MapGet("/", () => "Hello World!");

app.Run();
