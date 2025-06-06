using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssemblies(assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
{
	opt.Connection(builder.Configuration.GetConnectionString("Database"));

}).UseLightweightSessions();

//Seeding data to catalog db
if (builder.Environment.IsDevelopment())
	builder.Services.InitializeMartenWith<CatalogInitialData>();

//Register Custom exception Handling
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Register health check service
builder.Services.AddHealthChecks()
	.AddNpgSql(builder.Configuration.GetConnectionString("Database"));




var app = builder.Build(); 
//Configure the HTTP request pipline
app.MapCarter();

#region exceptionHandling
//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//	exceptionHandlerApp.Run(async context =>
//	{
//		var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//		if (exception == null) { return; }

//		var problemDetails = new ProblemDetails
//		{
//			Title = exception.Message,
//			Status = StatusCodes.Status500InternalServerError,
//			Detail = exception.StackTrace 
//		};

//		var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//		logger.LogError(exception, exception.Message);
//		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//		context.Response.ContentType = "application/problem+json";

//		await context.Response.WriteAsJsonAsync(problemDetails);

//	});
//});

app.UseExceptionHandler(opt => { });
#endregion
app.UseHealthChecks("/health", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}
 	);

app.Run();
