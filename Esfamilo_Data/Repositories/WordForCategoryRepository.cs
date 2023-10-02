using Esfamilo_Data.Context;
using Esfamilo_Domain.Interfaces;
using Esfamilo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Data.Repositories
{
    public class WordForCategoryRepository : IWordForCategoryRepository
    {
        private EsfamiloDbContext _context;
        public WordForCategoryRepository(EsfamiloDbContext context)
        {
            _context = context;
        }

        public async Task<WordForCategory> Add(WordForCategory entity)
        {
            var Entity = await _context.WordForCategories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Entity.Entity;
        }

        public async Task Delete(WordForCategory entity)
        {
            _context.WordForCategories.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var entity = await Get(Id);
            _context.WordForCategories.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<WordForCategory> Get(int Id)
        {
            var Entity = await _context.WordForCategories.FindAsync(Id);
            if (Entity != null)
                return Entity;
            else
                return null;
        }

        public async Task<IEnumerable<WordForCategory>> GetAll()
        {
            var Entities = await _context.WordForCategories.ToListAsync();
            return Entities;
        }

        public async Task Update(WordForCategory entity)
        {
            _context.WordForCategories.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
