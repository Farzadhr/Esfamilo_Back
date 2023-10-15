using Esfamilo_Domain.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Domain.Models
{
    public class WordForCategory : BaseEntity
    {
        public string Word { get; set; }
        public string TargetLetter { get; set; }
        public bool IsCorrect { get; set; }
        public int WordScore{ get; set; }
        public int LobbyRound { get; set; }
        public int CategoryId{ get; set; }
        public string UserId { get; set; }
        public int LobbyId { get; set; }
        [ForeignKey("LobbyId")]
        public Lobby lobby { get; set; }
    }
}
