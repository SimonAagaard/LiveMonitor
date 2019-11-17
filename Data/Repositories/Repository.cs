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

            if (entities.Any())
            {
                return entities;
            }

            throw new Exception("The table that holds " + typeof(Repository<T>) + " is empty");
        }

        public async Task<T> Get(Guid id)
        {
            T entity = await _entities.FindAsync(id);

            if (entity != null)
            {
                return entity;
            }

            throw new Exception("An entity with this ID(" + entity.Id + ") does not exist!");
        }

        // Find List of entities based on a predicate.
        public async Task<List<T>> GetMany(Expression<Func<T, bool>> predicate)
        {
            List<T> entities = await _entities.Where(predicate).ToListAsync();

            if (entities.Any())
            {
                return entities;
            }

            throw new Exception("The table that holds " + typeof(Repository<T>) + " is empty, with predicate: " + predicate);
        }

        // Validation here?
        public async Task Add(T entity)
        {
            if (entity.Id != Guid.Empty)
            {
                await _entities.AddAsync(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Entity of type " + typeof(Repository<T>) + " was not valid, and has not been created");
            }
        }

        // Generic update method. Checks for valid object before updating
        public async Task Update(T entity)
        {
            T entityToUpdate = await Get(entity.Id);
            
            if (entityToUpdate != null)
            {
                _entities.Update(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("An entity with this ID(" + entity.Id + ") does not exist!");
            }
        }

        // Generic harddeletion of an object. Checks that an objects exist before attempting to delete.
        // Create a soft delete to make use of IsDeleted.
        public async Task Delete(Guid id)
        {
            T entity = await Get(id);

            if (entity != null)
            {
                _entities.Remove(entity);
                _context.SaveChanges();
            }

            throw new Exception("An entity with this ID (" + id + ") does not exist!");
        }
    }
}