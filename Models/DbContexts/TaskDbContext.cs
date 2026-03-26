using TaskTracker.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Server.Models.DbContexts;

public class TaskDbContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>(builder => 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).HasMaxLength(200);
        });
    }
}   
