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
    public class CategoryInLobbyService : ICategoryInLobbyService
    {
        private ICategoryInLobbyRepository _repository;
        public CategoryInLobbyService(ICategoryInLobbyRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryInLobby> Add(CategoryInLobby entity)
        {
            var Entity = await _repository.Add(entity);
            return Entity;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<CategoryInLobby> Get(int Id)
        {
            var Entity = await _repository.Get(Id);
            return Entity;
        }

        public async Task<IEnumerable<CategoryInLobby>> GetAll()
        {
            var Entities = await _repository.GetAll();
            return Entities;
        }

        public async Task Update(CategoryInLobby entity)
        {
            await _repository.Update(entity);
        }
    }
}
