using Esfamilo_Core.Interfaces;
using Esfamilo_Core.ModelView;
using Esfamilo_Core.Services;
using Esfamilo_Domain.Models;
using Esfamilo_Web.Data;
using Esfamilo_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Esfamilo_Web.Hubs
{
    public class LobbyHub : Hub
    {
        private ILobbyService lobbyService;
        private ICategoryInLobbyService categoryInLobbyService;
        private ICategoryService _category;
        private IUserInLobbyService userInLobby;
        private UserManager<IdentityUser> _userManager;
        private IHttpContextAccessor httpContextAccessor;
        private ApplicationDbContext _context;
        private IWordForCategoryService wordService;
        private IHubContext<ManageLobbyHub> _manageLobbyHubContext;
        public LobbyHub(ILobbyService lobbyService, ICategoryInLobbyService wordForCategoryService, ICategoryService service, UserManager<IdentityUser> userManager, IUserInLobbyService userInLobby, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, IWordForCategoryService wordForCategoryService1, IHubContext<ManageLobbyHub> manageLobbyHubContext)
        {
            this.lobbyService = lobbyService;
            categoryInLobbyService = wordForCategoryService;
            _category = service;
            _userManager = userManager;
            this.userInLobby = userInLobby;
            this.httpContextAccessor = httpContextAccessor;
            _context = context;
            wordService = wordForCategoryService1;
            _manageLobbyHubContext = manageLobbyHubContext;
        }


        public override async Task OnConnectedAsync()
        {
            ApplicationUser getUser = (ApplicationUser)await _userManager.GetUserAsync(Context.User);
            var httpContext = Context.GetHttpContext();
            var lobbyuid = httpContext.Request.Query["LobbyUID"];
            var Lobby = await lobbyService.GetLobbyWithUID(lobbyuid);
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyuid.ToString());
            if (Lobby.InGameStatus == false)
            {
                var currentUsesUserInLobby = await userInLobby.GetUserInLobbyByUserId(getUser.Id);
                if (currentUsesUserInLobby == null)
                {
                    await userInLobby.Add(new UserInLobby
                    {
                        UserId = getUser.Id,
                        IsUserOwner = false,
                        UserScore = 0,
                        LobbyId = Lobby.Id,
                    });
                }
                //else
                //{
                //    if(currentUsesUserInLobby.IsUserOwner == false)
                //    {
                //        await userInLobby.Delete(currentUsesUserInLobby.Id);
                //        await userInLobby.Add(new UserInLobby
                //        {
                //            UserId = getUser.Id,
                //            IsUserOwner = false,
                //            UserScore = 0,
                //            LobbyId = Lobby.Id,
                //        });
                //    }
                //}
                var GetUserInLobby = await userInLobby.GetUserInLobbybyLobbyID(Lobby.Id);
                if (GetUserInLobby.Count == Lobby.LimitUserCount)
                {
                    var limitedLobby = Lobby;
                    limitedLobby.IsLimitLobby = true;
                    await lobbyService.Update(limitedLobby);

                    var UpdateUnLimitedLobbiesList = await lobbyService.GetAllUnLimitesLobby();
                    List<LobbyForList> getlobbyforlist = new List<LobbyForList>();
                    foreach (var lobb in UpdateUnLimitedLobbiesList)
                    {
                        var userlengthinlobby = await userInLobby.GetUserInLobbybyLobbyID(lobb.Id);
                        getlobbyforlist.Add(new LobbyForList
                        {
                            LobbyGuid = lobb.LobbyGuid,
                            LobbyName = lobb.LobbyName,
                            CountUserInLobby = userlengthinlobby.Count.ToString(),
                            LimitUserCount = lobb.LimitUserCount
                        });
                    }

                    await _manageLobbyHubContext.Clients.All.SendAsync("ConnectedUserGetLobbies", JsonConvert.SerializeObject(getlobbyforlist));
                }
                if (GetUserInLobby.Count > Lobby.LimitUserCount)
                {
                    if (currentUsesUserInLobby != null)
                    {
                        await userInLobby.Delete(currentUsesUserInLobby.Id);
                    }
                    await Clients.Caller.SendAsync("GetOutUnLimitedUser");
                }
            }
            else
            {
                var GetUserInLobby = await userInLobby.GetUserInLobbybyLobbyID(Lobby.Id);
                if (userInLobby.GetUserInLobbyByUserId(getUser.Id).Result.IsUserOwner == true)
                {
                    if (Lobby.CurrentRound + 1 <= Lobby.RoundCount)
                    {
                        Lobby.CurrentRound += 1;
                        await lobbyService.Update(Lobby);
                        string updatedCurrentRound = $"{Lobby.RoundCount}/{Lobby.CurrentRound}";
                        await Clients.Group(lobbyuid.ToString()).SendAsync("updateCurrentRound" , updatedCurrentRound);
                    }
                    else
                    {
                        await Clients.Group(lobbyuid.ToString()).SendAsync("LobbyStopGame");
                    }
                }
                if (GetUserInLobby.Count == Lobby.LimitUserCount)
                {
                    Lobby.InGameStatus = false;
                    await lobbyService.Update(Lobby);
                }
            }
            var UsersinLobby = await userInLobby.GetUserInLobbybyLobbyID(Lobby.Id);
            List<UserInLobbyForLobby> userInLobbyForLobbies = new List<UserInLobbyForLobby>();
            foreach (var user in UsersinLobby)
            {
                userInLobbyForLobbies.Add(new UserInLobbyForLobby
                {
                    UserId = user.UserId,
                    UserName = GetUserById(user.UserId).Result.UserName,
                    IsOwner = user.IsUserOwner,
                    UserScore = user.UserScore.ToString()
                });
            }
            await Clients.Group(Lobby.LobbyGuid).SendAsync("CheckUsersInLobbies", JsonConvert.SerializeObject(userInLobbyForLobbies));
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var lobbyuid = Context.GetHttpContext().Request.Query["LobbyUID"];
            var Lobby = await lobbyService.GetLobbyWithUID(lobbyuid);
            if (Lobby != null)
            {
                var lobbyIsGameStatus = Lobby.InGameStatus;
                if (lobbyIsGameStatus == false)
                {
                    ApplicationUser cuser = await _userManager.GetUserAsync(Context.User) as ApplicationUser;
                    var getUserInLobby = await userInLobby.GetUserInLobbyByUserId(cuser.Id);
                    if (getUserInLobby != null)
                    {
                        var lobby = await lobbyService.Get(getUserInLobby.LobbyId);
                        if (getUserInLobby.IsUserOwner == false)
                        {
                            await userInLobby.Delete(getUserInLobby.Id);
                            await Clients.Caller.SendAsync("OutAllLobbyUser");
                            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyuid.ToString());
                        }
                        else
                        {
                            await lobbyService.Delete(getUserInLobby.LobbyId);
                            await Clients.Group(lobby.LobbyGuid).SendAsync("OutAllLobbyUser");
                            await Clients.Caller.SendAsync("OutAllLobbyUser");
                            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyuid.ToString());
                        }
                    }


                }
            }

            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string message)
        {
            var lobbyuid = Context.GetHttpContext().Request.Query["LobbyUID"];
            ApplicationUser senderuser = await _userManager.GetUserAsync(Context.User) as ApplicationUser;
            var currentUsesUserInLobby = await userInLobby.GetUserInLobbyByUserId(senderuser.Id);
            var SendMessage = new ChatMessageInLobby
            {
                UserId = senderuser.Id,
                SenderName = senderuser.UserName,
                LobbyId = currentUsesUserInLobby.LobbyId,
                Message = message
            };
            await Clients.Caller.SendAsync("CheckSenderMessage", JsonConvert.SerializeObject(SendMessage), true);
            await Clients.OthersInGroup(lobbyuid.ToString()).SendAsync("CheckClientMessage", JsonConvert.SerializeObject(SendMessage));
        }
        public async Task GoUsersToRoom()
        {
            var lobbyuid = Context.GetHttpContext().Request.Query["LobbyUID"];
            var lobby = await lobbyService.GetLobbyWithUID(lobbyuid);
            await lobbyService.ChangeIsGameStatus(true, lobby.Id);
            var getusedtargetletter = await wordService.GetUsedTargetLetter(lobby.Id);
            var targetletter = RandomLetter.GetRandomLetter(getusedtargetletter);
            var GotoGameUrl = $"/Game/{lobbyuid.ToString()}/{targetletter}";
            await Clients.Group(lobbyuid.ToString()).SendAsync("GotoGameAllUserLobby", GotoGameUrl);
        }
        public async Task ExitLobbyCustom()
        {
            var lobbyuid = Context.GetHttpContext().Request.Query["LobbyUID"];
            var lobbyIsGameStatus = lobbyService.GetLobbyWithUID(lobbyuid).Result.InGameStatus;
            if (lobbyIsGameStatus == false)
            {
                ApplicationUser cuser = await _userManager.GetUserAsync(Context.User) as ApplicationUser;
                var getUserInLobby = await userInLobby.GetUserInLobbyByUserId(cuser.Id);
                if (getUserInLobby != null)
                {
                    var lobby = await lobbyService.Get(getUserInLobby.LobbyId);
                    if (getUserInLobby.IsUserOwner == false)
                    {
                        await userInLobby.Delete(getUserInLobby.Id);
                        await Clients.Caller.SendAsync("OutAllLobbyUser");
                        await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyuid.ToString());
                    }
                    else
                    {
                        await lobbyService.Delete(getUserInLobby.LobbyId);
                        await Clients.Group(lobby.LobbyGuid).SendAsync("OutAllLobbyUser");
                        await Clients.Caller.SendAsync("OutAllLobbyUser");
                        await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyuid.ToString());
                    }
                }


            }
        }
        public async Task<ApplicationUser> GetUserById(string userid)
        {
            var user = await _context.AppUser.FindAsync(userid);
            return user;
        }
    }
}
