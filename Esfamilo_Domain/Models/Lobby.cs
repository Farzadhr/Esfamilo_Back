﻿using Esfamilo_Domain.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int LimitUserCount { get; set; }
        public bool IsLimitLobby { get; set; }
        public bool InGameStatus { get; set; }
        public virtual List<UserInLobby> UserInLobbies { get; set; }
        public virtual List<WordForCategory> WordForCategories { get; set; }
        public virtual List<CategoryInLobby> CategoryInLobbies { get; set; }
        public virtual List<ChatMessageInLobby> ChatMessageInLobbies { get; set; }
    }
}
