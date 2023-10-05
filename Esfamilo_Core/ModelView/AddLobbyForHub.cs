using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.ModelView
{
    public class AddLobbyForHub
    {
        public string LobbyName { get; set; }
        public bool IsPrivateLobby { get; set; }
        public int RoundCount { get; set; }
        public string CategorySelected { get; set; }
        public int LimitUserCount { get; set; }
    }
}
