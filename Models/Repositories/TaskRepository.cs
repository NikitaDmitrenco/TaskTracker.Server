using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TaskTracker.Server.Models.DbContexts;
using TaskTracker.Server.Models.Entities;

namespace TaskTracker.Server.Models.Repositories
{
    public class TaskRepository : BaseRepository<TaskDbContext, TaskEntity>
    {
        private readonly IDbContextFactory<TaskDbContext> factory;
        public TaskRepository(IDbContextFactory<TaskDbContext> factory) : base(factory)
        {
            this.factory = factory ?? throw new InvalidOperationException
                                        ($"No service for type {typeof(IDbContextFactory<TaskDbContext>)} was provided");
        }

        public override async Task Update(TaskEntity taskEntity) 
        {
            using (var context = await factory.CreateDbContextAsync())
            {
                var dbEntity = await context.Tasks.FirstOrDefaultAsync(x => x.Id == taskEntity.Id);

                if (dbEntity == null) 
                {
                    throw new InvalidOperationException($"Failed to find entity in database");
                }

                dbEntity.Description = taskEntity.Description;
                dbEntity.IsExecuted = taskEntity.IsExecuted;

                await context.SaveChangesAsync();
            }
        }
    }
}
