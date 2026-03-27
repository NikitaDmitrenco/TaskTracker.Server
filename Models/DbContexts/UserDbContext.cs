using Microsoft.EntityFrameworkCore;
using TaskTracker.Server.Models.Entities;

namespace TaskTracker.Server.Models.DbContexts;

public class UserDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        
        modelBuilder.Entity<UserEntity>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Tasks)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .HasPrincipalKey(x => x.Id);
        });
    }
}
