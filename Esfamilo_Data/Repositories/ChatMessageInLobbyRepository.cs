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
    public class ChatMessageInLobbyRepository : IChatMessageInLobbyRepository
    {
        private EsfamiloDbContext _context;
        public ChatMessageInLobbyRepository(EsfamiloDbContext context)
        {
            _context = context;
        }

        public async Task<ChatMessageInLobby> Add(ChatMessageInLobby entity)
        {
            var Entity = await _context.chatMessageInLobbies.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Entity.Entity;
        }

        public async Task Delete(ChatMessageInLobby entity)
        {
            _context.chatMessageInLobbies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var entity = await Get(Id);
            _context.chatMessageInLobbies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ChatMessageInLobby> Get(int Id)
        {
            var Entity = await _context.chatMessageInLobbies.FindAsync(Id);
            if (Entity != null)
                return Entity;
            else
                return null;
        }

        public async Task<IEnumerable<ChatMessageInLobby>> GetAll()
        {
            var Entities = await _context.chatMessageInLobbies.ToListAsync();
            return Entities;
        }

        public async Task Update(ChatMessageInLobby entity)
        {
            _context.chatMessageInLobbies.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
