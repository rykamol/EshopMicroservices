using BuildingBlocks.Exceptions.Handler;
using JasperFx;
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
	options.Configuration = builder.Configuration.GetConnectionString("Radis");
});
//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//	var basketRepository = provider.GetRequiredService<BasketRepository>(); ;
//	return new CacheBasketRepository(basketRepository,provider.GetRequiredService<IDistributedCache>());
//});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();
//Configure the HTTP request
app.MapCarter();
app.UseExceptionHandler(e => { });

app.Run();
