using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using JasperFx;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly); //register our mediator into current assembly
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(opt =>
{
	opt.Connection(builder.Configuration.GetConnectionString("Database"));
	//opt.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
	opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//	var basketRepository = provider.GetRequiredService<BasketRepository>(); ;
//	return new CacheBasketRepository(basketRepository,provider.GetRequiredService<IDistributedCache>());
//});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Register health check service
builder.Services.AddHealthChecks()
	.AddNpgSql(builder.Configuration.GetConnectionString("Database"))
	.AddRedis(builder.Configuration.GetConnectionString("Redis"));

var app = builder.Build();
//Configure the HTTP request
app.MapCarter();
app.UseExceptionHandler(e => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
