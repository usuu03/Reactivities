using System;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Persistence;

// Inherits from DbContext class
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // Create a table called Activities in the database based on the Activity class
    public DbSet<Activity> Activities { get; set; }
}
