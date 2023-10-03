using Esfamilo_Data.Context;
using Esfamilo_Domain.Interfaces;
using Esfamilo_Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Data.Repositories
{
    public class CategoryInLobbyRepository : ICategoryInLobbyRepository
    {
        private EsfamiloDbContext _context;
        public CategoryInLobbyRepository(EsfamiloDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryInLobby> Add(CategoryInLobby entity)
        {
            var Entitiy = await _context.categoryInLobbies.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Entitiy.Entity;
        }

        public async Task Delete(CategoryInLobby entity)
        {
            _context.categoryInLobbies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var entity = await Get(Id);
            if (entity != null)
            {
                _context.categoryInLobbies.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CategoryInLobby> Get(int Id)
        {
            var entity = await _context.categoryInLobbies.FindAsync(Id);
            return entity;
        }

        public async Task<IEnumerable<CategoryInLobby>> GetAll()
        {
            var Entities = await _context.categoryInLobbies.ToListAsync();
            return Entities;
        }

        public async Task Update(CategoryInLobby entity)
        {
            _context.categoryInLobbies.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
