using Microsoft.EntityFrameworkCore;
using SalesManager.Repository.Context;

namespace SalesManager.Repository.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly SalesManagerDbContext _context;
        public BaseRepository(SalesManagerDbContext context)
        {
            _context = context;
        }

        public void Add(T Ent)
        {
            _context.AddAsync(Ent);
        }

        public void Update(T Ent)
        {
            _context.Update(Ent);
        }

        public void Delete(T Ent)
        {
            _context.Remove(Ent);
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task<T[]> ListAsync()
        {
            return await _context.Set<T>().ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}