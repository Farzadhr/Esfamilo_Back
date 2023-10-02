using Esfamilo_Data.Context;
using Esfamilo_Domain.Interfaces;
using Esfamilo_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Data.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private EsfamiloDbContext _context;
        public FriendRepository(EsfamiloDbContext context)
        {
            _context = context;
        }

        public async Task<Friend> Add(Friend entity)
        {
            var Entity = await _context.friends.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Entity.Entity;
        }

        public async Task Delete(Friend entity)
        {
            _context.friends.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var entity = await Get(Id);
            _context.friends.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Friend> Get(int Id)
        {
            var Entity = await _context.friends.FindAsync(Id);
            if (Entity != null)
                return Entity;
            else
                return null;
        }

        public async Task<IEnumerable<Friend>> GetAll()
        {
            var Entities = await _context.friends.ToListAsync();
            return Entities;
        }

        public async Task Update(Friend entity)
        {
            _context.friends.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
