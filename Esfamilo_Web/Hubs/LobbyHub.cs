using Esfamilo_Core.Interfaces;
using Esfamilo_Core.ModelView;
using Esfamilo_Domain.Models;
using Esfamilo_Web.Data;
using Esfamilo_Web.Models;
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
        public LobbyHub(ILobbyService lobbyService, ICategoryInLobbyService wordForCategoryService, ICategoryService service, UserManager<IdentityUser> userManager, IUserInLobbyService userInLobby, IHttpContextAccessor httpContextAccessor,ApplicationDbContext context)
        {
            this.lobbyService = lobbyService;
            categoryInLobbyService = wordForCategoryService;
            _category = service;
            _userManager = userManager;
            this.userInLobby = userInLobby;
            this.httpContextAccessor = httpContextAccessor;
            _context= context;
        }
        public override async Task OnConnectedAsync()
        {
            ApplicationUser getUser = (ApplicationUser)await _userManager.GetUserAsync(Context.User);
            var httpContext = Context.GetHttpContext();
            var lobbyuid = httpContext.Request.Query["LobbyUID"];
            var Lobby = await lobbyService.GetLobbyWithUID(lobbyuid);
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
            await Groups.AddToGroupAsync(Context.ConnectionId, Lobby.LobbyGuid);
            var UsersinLobby = await userInLobby.GetUserInLobbybyLobbyID(Lobby.Id);
            List<UserInLobbyForLobby> userInLobbyForLobbies= new List<UserInLobbyForLobby>();
            foreach(var user in UsersinLobby)
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
            ApplicationUser cuser = await _userManager.GetUserAsync(Context.User) as ApplicationUser;
            var getUserInLobby = await userInLobby.GetUserInLobbyByUserId(cuser.Id);
            if(getUserInLobby != null)
            {
                var lobby = await lobbyService.Get(getUserInLobby.LobbyId);
                if (getUserInLobby.IsUserOwner == false)
                {
                    await userInLobby.Delete(getUserInLobby.Id);
                }
                else
                {
                    await Clients.Group(lobby.LobbyGuid).SendAsync("OutAllLobbyUser");
                    await lobbyService.Delete(getUserInLobby.LobbyId);
                }
            }


            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string message)
        {
            ApplicationUser senderuser = await _userManager.GetUserAsync(Context.User) as ApplicationUser;
            var currentUsesUserInLobby = await userInLobby.GetUserInLobbyByUserId(senderuser.Id);
            var SendMessage = new ChatMessageInLobby
            {
                UserId = senderuser.Id,
                SenderName = senderuser.UserName,
                LobbyId = currentUsesUserInLobby.LobbyId,
                Message = message
            };
            await Clients.Caller.SendAsync("CheckSenderMessage",JsonConvert.SerializeObject(SendMessage),true);
            await Clients.Others.SendAsync("CheckClientMessage", JsonConvert.SerializeObject(SendMessage));
        }
        public async Task<ApplicationUser> GetUserById(string userid)
        {
            var user = await _context.AppUser.FindAsync(userid);
            return user;
        }
    }
}
