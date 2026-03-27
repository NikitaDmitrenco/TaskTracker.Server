using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskTracker.Server.Models.DbContexts;
using TaskTracker.Server.Models.Entities;

namespace TaskTracker.Server.Models.Repositories
{
    public class UserRepository : BaseRepository<UserDbContext, UserEntity>
    {
        public readonly IDbContextFactory<UserDbContext> factory;
        public UserRepository(IDbContextFactory<UserDbContext> factory) : base(factory)
        {
            this.factory = factory ?? throw new InvalidOperationException
                                        ($"No service for type {typeof(IDbContextFactory<UserDbContext>)} was provided");
        }

        public override async Task Update(UserEntity userEntity)
        {
            using (var context = await factory.CreateDbContextAsync())
            {
                var dbEntity = await context.Users.FirstOrDefaultAsync(x => x.Id == userEntity.Id);

                if (dbEntity == null)
                {
                    throw new InvalidOperationException($"Failed to find entity in database");
                }

                dbEntity.Name = userEntity.Name;
                dbEntity.Email = userEntity.Email;

                await context.SaveChangesAsync();
            }
        }
    }
}
