using Esfamilo_Domain.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Domain.Models
{
    public class UserInLobby : BaseEntity
    {
        public string UserId { get; set; }
        public int UserScore { get; set; }
        public bool IsUserOwner { get; set; }
        public int LobbyId { get; set; }
        public Lobby lobby { get; set; }
    }
}
