using Esfamilo_Core.Interfaces;
using Esfamilo_Core.ModelView;
using Esfamilo_Core.Utilities;
using Esfamilo_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Esfamilo_Web.Hubs
{
    public class ManageLobbyHub : Hub
    {
        private ILobbyService lobbyService;
        private ICategoryInLobbyService categoryInLobbyService;
        private ICategoryService _category;
        private IUserInLobbyService userInLobby;
        private UserManager<IdentityUser> _userManager;
        public ManageLobbyHub(ILobbyService lobbyService, ICategoryInLobbyService wordForCategoryService, ICategoryService service, UserManager<IdentityUser> userManager, IUserInLobbyService userInLobby)
        {
            this.lobbyService = lobbyService;
            categoryInLobbyService = wordForCategoryService;
            _category = service;
            _userManager = userManager;
            this.userInLobby = userInLobby;
        }
        public async override Task OnConnectedAsync()
        {
            var lobbies = await lobbyService.GetAll();
            List<LobbyForList> getlobbyforlist = new List<LobbyForList>();
            foreach(var lobb in lobbies)
            {
                var userlengthinlobby = await userInLobby.GetUserInLobbybyLobbyID(lobb.Id);
                getlobbyforlist.Add(new LobbyForList
                {
                    LobbyGuid= lobb.LobbyGuid,
                    LobbyName = lobb.LobbyName,
                    CountUserInLobby = userlengthinlobby.Count.ToString(),
                    LimitUserCount = lobb.LimitUserCount
                });
            }

            await Clients.Caller.SendAsync("ConnectedUserGetLobbies",JsonConvert.SerializeObject(getlobbyforlist));
            await base.OnConnectedAsync();
        }
        public async Task CreateLobby(string data)
        {
            AddLobbyForHub AddLobby = JsonConvert.DeserializeObject<AddLobbyForHub>(data);
            var lobby = await lobbyService.Add(new Lobby
            {
                LobbyName = AddLobby.LobbyName,
                LobbyGuid = RandomUID.GetRandomUID(),
                RoundCount = AddLobby.RoundCount,
                CurrentRound = 1,
                LimitUserCount = AddLobby.LimitUserCount,
            });



            var categoryselected = AddLobby.CategorySelected.Split(',');
            foreach (var category in categoryselected)
            {
                var cateId = await _category.GetByCateName(category);
                await categoryInLobbyService.Add(new CategoryInLobby
                {
                    CategoryId = cateId.Id,
                    LobbyId = lobby.Id
                });
            }
            var currentUser = await _userManager.GetUserAsync(Context.User);
            await userInLobby.Add(new UserInLobby
            {
                LobbyId = lobby.Id,
                UserId = currentUser.Id,
                UserScore = 0,
                IsUserOwner = true,
            });
            var userlengthinlobby = await userInLobby.GetUserInLobbybyLobbyID(lobby.Id);
            var SenderData = JsonConvert.SerializeObject(new LobbyForList
            {
                LobbyName = lobby.LobbyName,
                LobbyGuid = lobby.LobbyGuid,
                CountUserInLobby = userlengthinlobby.Count.ToString(),
                LimitUserCount = lobby.LimitUserCount
            });
            await Clients.All.SendAsync("CheckLobby", SenderData);
            await Clients.Caller.SendAsync("SendOwnerToLobby", lobby.LobbyGuid);
        }
    }
}
