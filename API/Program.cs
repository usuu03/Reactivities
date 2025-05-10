using API.Middleware;
using Application.Activities.Commands;
using Application.Activities.Queries;
using Application.Activities.Validators;
using Application.Common;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

// Sets up the App configuration
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registers Controllers as services tells .NET to look for controllers and use them for HTTP requests
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

// Tells .NET to use AppDbContext to talk to the database
// and use SQLite as the database provider. The connection string is read from the appsettings.json file.
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors();

// Adds the MediatR service to the DI container
builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>();
    x.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

// Adds the AutoMapper service to the DI container
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

// Adds the FluentValidation service to the DI container
builder.Services.AddValidatorsFromAssemblyContaining<CreateActivityValidator>();

// Middleware for handling exceptions globally
// Only created when needed, not at startup
builder.Services.AddTransient<ExceptionMiddleware>();

// Adds the Identity service to the DI container
// Configures Identity to use the User class and the AppDbContext class
builder.Services.AddIdentityApiEndpoints<User>(opt =>
{
    opt.User.RequireUniqueEmail = true;

})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

// Builds the app
var app = builder.Build();

// Middleware for handling exceptions globally
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x

    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins(
        "http://localhost:3000",
        "https://localhost:3000"
    )
);

// Middleware for authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Maps routes like /api/activites to controller methods.
// similar to defining URLs in Django
app.MapControllers();
app.MapGroup("api").MapIdentityApi<User>(); // api/login


// Creates a scope so we can get services like the database context
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;


try
{
    // Gets the AppDbContext 
    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    // Applies any pending to the database 
    await context.Database.MigrateAsync();
    //Adds any initial data to the database
    await DbInitializer.SeedData(context, userManager);
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
