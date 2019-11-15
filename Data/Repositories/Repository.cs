using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbContext _context;
        private DbSet<T> _entities;

        public Repository()
        {
            _context = new DbContext(new DbContextOptions<DbContext>());
            _entities = _context.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> Get(string userId)
        {
            var results = await _entities
                .FirstOrDefaultAsync(d => d.Id.ToString() == userId);
            return results;
        }

        public async Task<T> Get(Guid id)
        {
            var results = await _entities
                .FirstOrDefaultAsync(d => d.Id == id);
            return results;
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
            _context.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            var entity = await _entities
                .FirstOrDefaultAsync(d => d.Id == id);
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public async Task Update(T entity)
        {
            var entityToUpdate = await _entities
                            .FirstOrDefaultAsync(d => d.Id == entity.Id);
            if(entity != null)
            {
                _entities.Update(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }

        //NICE TO HAVE - LAV DEN LASSE
        //public async Task<T> GetSpecificDashboardForUser(string userId, Guid id)
        //{
        //    var result = await entities
        //        .Where(d => d.Id == id)
        //        .Where(d => d.UserId == userId).FirstOrDefaultAsync();
        //    return result;
        //}
    }
}