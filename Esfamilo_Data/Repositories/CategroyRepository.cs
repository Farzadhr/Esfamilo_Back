using Esfamilo_Data.Context;
using Esfamilo_Domain.Interfaces;
using Esfamilo_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Esfamilo_Data.Repositories
{
    public class CategroyRepository : ICategroyRepository
    {
        private EsfamiloDbContext _context;
        public CategroyRepository(EsfamiloDbContext context)
        {
            _context= context;
        }

        public async Task<Category> Add(Category entity)
        {
            var Entity = await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Entity.Entity;
        }

        public async Task Delete(Category entity)
        {
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var entity = await Get(Id);
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> Get(int Id)
        {
            var Entity = await _context.Categories.FindAsync(Id);
            if (Entity != null)
                return Entity;
            else
                return null;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var Entities = await _context.Categories.ToListAsync();
            return Entities;
        }

        public async Task Update(Category entity)
        {
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
