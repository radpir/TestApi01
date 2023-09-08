using Domain.Data;
using Domain.WorkTasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Helpers;
using System.Reflection;

namespace Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    protected readonly IConfiguration Configuration;

    public ApplicationDbContext() { }
    public ApplicationDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //var connectionString = Configuration.GetConnectionString("RailwayAppDb");
        var connectionString = ConnectionHelper.GetConnectionString(Configuration);
        // connect to postgres with connection string from app settings
        options.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<WorkTask> WorkTasks { get; set; }
}
