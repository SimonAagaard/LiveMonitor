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

        // Get all objects of a type
        public async Task<List<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        //Get a single object of a type, based on its id
        public async Task<T> Get(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        // Get a single object based on a predicate
        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _entities.FirstOrDefaultAsync(predicate);
        }

        //Return a single object along with its child objects
        public async Task<T> Get(Expression<Func<T, bool>> predicate, string[] children)
        {
            IQueryable<T> entities = _entities;

            foreach (var entity in children)
            {
                entities = entities.Include(entity);
            }

            return await entities.FirstOrDefaultAsync(predicate);
        }

        // Find List of entities based on a predicate.
        public async Task<List<T>> GetMany(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        //Return list of objects along with child objects
        public async Task<List<T>> GetMany(Expression<Func<T, bool>> predicate, string[] children)
        {
            IQueryable<T> entities = _entities;

            foreach (var entity in children)
            {
                entities = entities.Include(entity);
            }
        
            return await entities.Where(predicate).ToListAsync();
        }

        // Validation here?
        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddMany(List<T> entities)
        {
            await _context.AddRangeAsync(entities);
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