using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

// Inherits from DbContext class
public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    // Create a table called Activities in the database based on the Activity class
    public DbSet<Activity> Activities { get; set; }
}
