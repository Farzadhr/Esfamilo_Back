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
    public class LobbyService : ILobbyService
    {
        private ILobbyRepository _repository;
        public LobbyService(ILobbyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Lobby> Add(Lobby entity)
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
                await _repository.Delete(Entity);
            }
            else
            {
                throw new Exception("Entity not found!");
            }
        }

        public async Task<Lobby> Get(int Id)
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

        public async Task<IEnumerable<Lobby>> GetAll()
        {
            var Entities = await _repository.GetAll();
            return Entities;
        }

        public async Task<Lobby> GetLobbyWithUID(string UID)
        {
            var enitity = await _repository.GetAll();
            return enitity.FirstOrDefault(x => x.LobbyGuid == UID);
        }

        public async Task<IEnumerable<UserInLobby>> GetUserInLobbiesFromLobby(int id)
        {
            var entities = await _repository.GetUserInLobbiesFromLobbies(id);
            return entities;
        }

        public async Task Update(Lobby entity)
        {
            await _repository.Update(entity);
        }
    }
}
