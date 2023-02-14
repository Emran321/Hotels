using Hotels.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotels.Services
{
    public class GenericHotelService<TEntity> : IGenericHotelService<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<TEntity> DbSet;

        public GenericHotelService(ApplicationDbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllItemsAsync()
        {
            return await DbSet.ToArrayAsync();
        }

        public async Task<TEntity> GetItemByIdAsync(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> SearchFor(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.Where(expression).ToArrayAsync();
        }

        public async Task CreateItemAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditItemAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteItemAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
