using Esfamilo_Core.Interfaces.Generic;
using Esfamilo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Interfaces
{
    public interface ILobbyService : IGenericService<Lobby>
    {
        Task<Lobby> GetLobbyWithUID(string UID);
        Task<IEnumerable<UserInLobby>> GetUserInLobbiesFromLobby(int id);
        Task ChangeIsGameStatus(bool isGame,int LobbyId);
    }
}
