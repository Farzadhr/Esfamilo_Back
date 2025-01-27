﻿using Esfamilo_Core.Interfaces;
using Esfamilo_Domain.Interfaces;
using Esfamilo_Domain.Models;
using Microsoft.ML.Transforms.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Services
{
    public class WordForCategoryService : IWordForCategoryService
    {
        private IWordForCategoryRepository _repository;
        public WordForCategoryService(IWordForCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<WordForCategory> Add(WordForCategory entity)
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

        public async Task<WordForCategory> Get(int Id)
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

        public async Task<IEnumerable<WordForCategory>> GetAll()
        {
            var Entities = await _repository.GetAll();
            return Entities;
        }

        public async Task<List<WordForCategory>> GetAllWordsFromLobby(int lobbyid, int CurrentRound)
        {
            var Entities = await _repository.GetAll();
            var WordsinLobby = Entities.Where(x=>x.LobbyId== lobbyid && x.LobbyRound == CurrentRound).ToList();
            return WordsinLobby;
        }

        public async Task<List<WordForCategory>> GetAllWordsFromUserId(string UserId)
        {
            var Entities = await _repository.GetAll();
            var WordsForUser = Entities.Where(x => x.UserId == UserId).ToList();
            return WordsForUser;
        }

        public async Task<string> GetUsedTargetLetter(int lobbyid)
        {
            var Entities = await _repository.GetAll();
            if(Entities.Count() > 0)
            {
                var getletters = Entities.Where(x => x.LobbyId == lobbyid).Select(x => x.TargetLetter).Distinct().ToList();
                return string.Join(",", getletters);
            }
            else
            {
                return "";
            }

        }

        public async Task Update(WordForCategory entity)
        {
            await _repository.Update(entity);
        }
    }
}
