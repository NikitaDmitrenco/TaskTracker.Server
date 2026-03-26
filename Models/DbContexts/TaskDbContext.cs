using TaskTracker.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Server.Models.DbContexts;

public class TaskDbContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }
    public TaskDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=mysecretpassword;Database=TaskTracker;");
    }
}   
