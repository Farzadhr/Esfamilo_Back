using Esfamilo_Core.Interfaces;
using Esfamilo_Domain.Interfaces;
using Esfamilo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Services
{
    public class ChatMessageInLobbyService : IChatMessageInLobbyService
    {
        private IChatMessageInLobbyService _repository;
        public ChatMessageInLobbyService(IChatMessageInLobbyService repository)
        {
            _repository = repository;
        }

        public async Task<ChatMessageInLobby> Add(ChatMessageInLobby entity)
        {
            if (entity == null)
            {
                return null;
            }
            var Entity = await _repository.Add(entity);
            return Entity;
        }

        public async Task Delete(int id)
        {
            var Entity = await _repository.Get(id);
            if (Entity != null)
            {
                await _repository.Delete(id);
            }
            else
            {
                throw new Exception("Entity not found!");
            }
        }

        public async Task<ChatMessageInLobby> Get(int Id)
        {
            var Entity = await _repository.Get(Id);
            if (Entity != null)
            {
                return Entity;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<ChatMessageInLobby>> GetAll()
        {
            var Entities = await _repository.GetAll();
            return Entities;
        }

        public async Task Update(ChatMessageInLobby entity)
        {
            await _repository.Update(entity);
        }
    }
}
