using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;

        public Repository()
        {
            _context = new DbContext(new DbContextOptions<DbContext>());
            _entities = _context.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            List<T> entities = await _entities.ToListAsync();
            return entities;
        }

        public async Task<T> Get(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        // Find List of entities based on a predicate.
        public async Task<List<T>> GetMany(Expression<Func<T, bool>> predicate)
        {
            List<T> entities = await _entities.Where(predicate).ToListAsync();
            return entities;
        }

        // Validation here?
        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Generic update method.
        public async Task Update(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Generic harddeletion of an object.
        // Create a soft delete to make use of IsDeleted.
        public async Task Delete(T entity)
        {
             _entities.Remove(entity);
             await _context.SaveChangesAsync();
        }
        public async Task Seed(T entity)
        {
             await _entities.AddAsync(entity);
             await _context.SaveChangesAsync();
        }
    }
}