using Framwork.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Framwork.Infrastructre
{
    public class RepositoryBase<TKEY, T> : IRepository<TKEY, T> where T : class
    {
        private readonly DbContext _context;
        public RepositoryBase(DbContext context) 
        {
            _context = context;
        
        }

        public async Task Add(T Entity)
        {
            await _context.AddAsync(Entity);
            await Save();
            
        }

        public void Delete(T Entity)
        {
            _context.Remove(Entity);
            Save();
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task<T> Get(TKEY id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


    }
}
