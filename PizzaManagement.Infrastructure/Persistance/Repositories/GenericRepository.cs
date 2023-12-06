using Microsoft.EntityFrameworkCore;
using PizzaManagement.Application.Common.Interfaces;

namespace PizzaManagement.Infrastructure.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly ApplicationDbContext _context;
        internal DbSet<T> DbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T item)
        {
            await DbSet.AddAsync(item);
            return item;
        }

        public async virtual Task<List<T>> AllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<bool> DeleteAsync(T item)
        {
            DbSet.Remove(item);
            return true;
        }

        public async virtual Task<T?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T item)
        {
            DbSet.Update(item);
            return true;
        }
    }
}