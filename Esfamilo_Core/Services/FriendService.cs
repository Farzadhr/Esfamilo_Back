﻿using Esfamilo_Core.Interfaces;
using Esfamilo_Domain.Interfaces;
using Esfamilo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Services
{
    public class FriendService : IFriendService
    {
        private IFriendRepository _repository;
        public FriendService(IFriendRepository repository)
        {
            _repository = repository;
        }

        public async Task<Friend> Add(Friend entity)
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

        public async Task<Friend> Get(int Id)
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

        public async Task<IEnumerable<Friend>> GetAll()
        {
            var Entities = await _repository.GetAll();
            return Entities;
        }

        public async Task Update(Friend entity)
        {
            await _repository.Update(entity);
        }
    }
}
