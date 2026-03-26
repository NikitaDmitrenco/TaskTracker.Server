using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TaskTracker.Server.Models.DbContexts;
using TaskTracker.Server.Models.Entities;

namespace TaskTracker.Server.Repositories
{
    public class TaskRepository
    {
        private IDbContextFactory<TaskDbContext> dbContextFactory;
        public TaskRepository(IDbContextFactory<TaskDbContext> dbContextFactory) 
        {
            this.dbContextFactory = dbContextFactory ?? throw new InvalidOperationException
                                        ($"No service for type {typeof(IDbContextFactory<TaskDbContext>)} was provided");
        }

        public async Task Add(TaskEntity taskEntity) 
        {
            using (var context = await dbContextFactory.CreateDbContextAsync())
            {
                await context.Tasks.AddAsync(new()
                {
                    Description = taskEntity.Description,
                    IsExecuted = taskEntity.IsExecuted
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task Update(TaskEntity taskEntity) 
        {
            using (var context = await dbContextFactory.CreateDbContextAsync())
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

        public async Task Delete(int id) 
        {
            using (var context = await dbContextFactory.CreateDbContextAsync())
            {
                var dbEntity = context.Tasks.FirstOrDefault(x => x.Id == id);

                if (dbEntity == null)
                {
                    throw new InvalidOperationException($"The entity must be already deleted from the database");
                }

                dbEntity.IsDeleted = true;

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<TaskEntity>> GetAll() 
        {
            using (var context = await dbContextFactory.CreateDbContextAsync()) 
            {
                var list = await context.Tasks.Where(x => !x.IsDeleted).ToListAsync();

                return list;
            }
        }

        public async Task<TaskEntity> Get(int id)
        {
            using (var context = await dbContextFactory.CreateDbContextAsync()) 
            {
                var entity = await context.Tasks.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

                if (entity == null) 
                {
                    throw new InvalidOperationException("Failed to find entity in the database");
                }

                return entity;
            }
        }
    }
}
