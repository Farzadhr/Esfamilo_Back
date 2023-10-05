using Esfamilo_Domain.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Domain.Models
{
    public class ChatMessageInLobby : BaseEntity
    {
        public string UserId { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set; }
        public int LobbyId { get; set; }
        [ForeignKey("LobbyId")]
        public virtual Lobby Lobby { get; set; }
    }
}
