var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssemblies(typeof(Program).Assembly);

});


var app = builder.Build();
//app.MapGet("/", () => "Hello World!");

app.MapCarter();

//Configure the HTTP request pipline



app.Run();
