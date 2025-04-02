using BuildingBlocks.Behaviors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssemblies(assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
{
	opt.Connection(builder.Configuration.GetConnectionString("Database"));
}).UseLightweightSessions();

var app = builder.Build();
//app.MapGet("/", () => "Hello World!");



//Configure the HTTP request pipline
app.MapCarter();

app.UseExceptionHandler(exceptionHandlerApp =>
{
	exceptionHandlerApp.Run(async context =>
	{
		var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
		if (exception == null) { return; }

		var problemDetails = new ProblemDetails
		{
			Title = exception.Message,
			Status = StatusCodes.Status500InternalServerError,
			Detail = exception.StackTrace 
		};

		var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
		logger.LogError(exception, exception.Message);
		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		context.Response.ContentType = "application/problem+json";

		await context.Response.WriteAsJsonAsync(problemDetails);

	});
});


app.Run();
