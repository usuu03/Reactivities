using Application.Activities.Queries;
using Application.Common;
using Microsoft.EntityFrameworkCore;
using Persistence;

// Sets up the App configuration
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registers Controllers as services tells .NET to look for controllers and use them for HTTP requests
builder.Services.AddControllers();

// Tells .NET to use AppDbContext to talk to the database
// and use SQLite as the database provider. The connection string is read from the appsettings.json file.
builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors();

// Adds the MediatR service to the DI container
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>());

// Adds the AutoMapper service to the DI container
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

// Builds the app
var app = builder.Build();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins(
        "http://localhost:3000",
        "https://localhost:3000",
        "https://3000s--main--reactivities-udemy--uedeagheomijie.coder.techary.com",
        "http://3000s--main--reactivities-udemy--uedeagheomijie.coder.techary.com"
    )
);

// Maps routes like /api/activites to controller methods.
// similar to defining URLs in Django
app.MapControllers();



// Creates a scope so we can get services like the database context
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;


try
{
    // Gets the AppDbContext 
    var context = services.GetRequiredService<AppDbContext>();
    // Applies any pending to the database 
    await context.Database.MigrateAsync();
    //Adds any initial data to the database
    await DbInitializer.SeedData(context);
}
// Log any errors that occur during migration or seeding
catch (System.Exception)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError("An error occurred during migration or seeding the database.");
    throw;
}

// Start the app and listen for incoming HTTP requests
app.Run();
