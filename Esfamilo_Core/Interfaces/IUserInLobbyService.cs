﻿using Esfamilo_Core.Interfaces.Generic;
using Esfamilo_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Interfaces
{
    public interface IUserInLobbyService : IGenericService<UserInLobby>
    {
        Task<List<UserInLobby>> GetUserInLobbybyLobbyID(int lobbyid);
        Task<UserInLobby> GetUserInLobbyByUserId(string userId);
    }
}
