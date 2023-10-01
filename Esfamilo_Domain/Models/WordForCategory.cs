using Esfamilo_Domain.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Domain.Models
{
    public class WordForCategory : BaseEntity
    {
        public string Word { get; set; }
        public bool IsCorrect { get; set; }
        public int WordScore{ get; set; }
        public int CategoryId{ get; set; }
        public int LobbyId { get; set; }
        public Lobby lobby { get; set; }
    }
}
