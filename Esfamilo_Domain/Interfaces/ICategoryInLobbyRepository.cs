using Esfamilo_Domain.Interfaces.Generic;
using Esfamilo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Domain.Interfaces
{
    public interface ICategoryInLobbyRepository : IGenericRepository<CategoryInLobby>
    {
        Task<IEnumerable<Category>> GetCategoryInLobbies(int lobbyId);
    }
}
