using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TaskTracker.Server.Models.DbContexts;
using TaskTracker.Server.Models.Entities;

namespace TaskTracker.Server.Models.Repositories
{
    public class BaseRepository<TContext, TEntity> where TContext : DbContext where TEntity : BaseEntity
    {
        private readonly IDbContextFactory<TContext> factory;
        
        public BaseRepository(IDbContextFactory<TContext> factory) 
        {
            this.factory = factory ?? throw new InvalidOperationException
                                        ($"No service for type {typeof(IDbContextFactory<TContext>)} was provided");
        }

        public virtual async Task Add(TEntity taskEntity)
        {
            using (var context = await factory.CreateDbContextAsync())
            {
                await context.Set<TEntity>().AddAsync(taskEntity);

                await context.SaveChangesAsync();
            }
        }

        public virtual async Task Update(TEntity taskEntity)
        {
            //using (var context = await factory.CreateDbContextAsync())
            //{
            //    var dbEntity = await context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == taskEntity.Id);

            //    if (dbEntity == null)
            //    {
            //        throw new InvalidOperationException($"Failed to find entity in database");
            //    }

            //    dbEntity.Description = taskEntity.Description;
            //    dbEntity.IsExecuted = taskEntity.IsExecuted;

            //    await context.SaveChangesAsync();
            //}
        }

        public virtual async Task Delete(int id)
        {
            using (var context = await factory.CreateDbContextAsync())
            {
                var dbEntity = context.Set<TEntity>().FirstOrDefault(x => x.Id == id);

                if (dbEntity == null)
                {
                    throw new InvalidOperationException($"The entity must be already deleted from the database");
                }

                dbEntity.IsDeleted = true;

                await context.SaveChangesAsync();
            }
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            using (var context = await factory.CreateDbContextAsync())
            {
                var list = await context.Set<TEntity>().Where(x => !x.IsDeleted).ToListAsync();

                return list;
            }
        }

        public virtual async Task<TEntity> Get(int id)
        {
            using (var context = await factory.CreateDbContextAsync())
            {
                var entity = await context.Set<TEntity>().FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

                if (entity == null)
                {
                    throw new InvalidOperationException("Failed to find entity in the database");
                }

                return entity;
            }
        }



    }
}

