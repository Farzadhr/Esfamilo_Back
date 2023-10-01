using Esfamilo_Domain.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Domain.Models
{
    public class Lobby : BaseEntity
    {
        public string LobbyGuid { get; set; }
        public string LobbyName { get; set; }
        public int RoundCount { get; set; }
        public int CurrentRound { get; set; }
        public List<UserInLobby> UserInLobbies { get; set; }
    }
}
