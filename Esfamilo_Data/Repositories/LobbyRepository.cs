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
    public class LobbyRepository : ILobbyRepository
    {
        private EsfamiloDbContext _context;
        public LobbyRepository(EsfamiloDbContext context)
        {
            _context = context;
        }

        public async Task<Lobby> Add(Lobby entity)
        {
            var Entity = await _context.Lobbies.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Entity.Entity;
        }

        public async Task Delete(Lobby entity)
        {
            _context.Lobbies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var entity = await Get(Id);
            _context.Lobbies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Lobby> Get(int Id)
        {
            var Entity = await _context.Lobbies.FindAsync(Id);
            if (Entity != null)
                return Entity;
            else
                return null;
        }

        public async Task<IEnumerable<Lobby>> GetAll()
        {
            var Entities = await _context.Lobbies.ToListAsync();
            return Entities;
        }

        public async Task<IEnumerable<UserInLobby>> GetUserInLobbiesFromLobbies(int lobbiesId)
        {
            var Entities = await _context.Lobbies.Where(x => x.Id == lobbiesId).Include(n => n.UserInLobbies).Select(x => x.UserInLobbies).ToListAsync();
            return Entities[0];
        }

        public async Task Update(Lobby entity)
        {
            _context.Lobbies.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
