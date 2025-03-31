var builder = WebApplication.CreateBuilder(args);

//Add services to the container


var app = builder.Build();
//app.MapGet("/", () => "Hello World!");

//Configure the HTTP request pipline



app.Run();
