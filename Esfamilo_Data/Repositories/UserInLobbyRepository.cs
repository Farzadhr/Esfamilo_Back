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
    public class UserInLobbyRepository : IUserInLobbyRepository
    {
        private EsfamiloDbContext _context;
        public UserInLobbyRepository(EsfamiloDbContext context)
        {
            _context = context;
        }

        public async Task<UserInLobby> Add(UserInLobby entity)
        {
            var Entity = await _context.userInLobbies.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Entity.Entity;
        }

        public async Task Delete(UserInLobby entity)
        {
            _context.userInLobbies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var entity = await Get(Id);
            _context.userInLobbies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserInLobby> Get(int Id)
        {
            var Entity = await _context.userInLobbies.FindAsync(Id);
            if (Entity != null)
                return Entity;
            else
                return null;
        }

        public async Task<IEnumerable<UserInLobby>> GetAll()
        {
            var Entities = await _context.userInLobbies.ToListAsync();
            return Entities;
        }

        public async Task Update(UserInLobby entity)
        {
            _context.userInLobbies.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
