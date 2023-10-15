using Esfamilo_Core.Interfaces.Generic;
using Esfamilo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Interfaces
{
    public interface IWordForCategoryService : IGenericService<WordForCategory>
    {
        Task<string> GetUsedTargetLetter(int lobbyid);
        Task<List<WordForCategory>> GetAllWordsFromLobby(int lobbyid);
        Task<List<WordForCategory>> GetAllWordsFromUserId(string UserId);
    }
}
