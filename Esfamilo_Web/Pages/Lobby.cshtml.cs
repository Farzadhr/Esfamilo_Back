using Esfamilo_Core.Interfaces;
using Esfamilo_Domain.Models;
using Esfamilo_Web.Data;
using Esfamilo_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Esfamilo_Web.Pages
{
    public class LobbyModel : PageModel
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private ILobbyService lobbyService;
        private ApplicationDbContext _context;

        public LobbyModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILobbyService lobbyService,ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.lobbyService = lobbyService;
            _context = context;
        }
        public ApplicationUser CurrentUser { get; set; }
        public Lobby thisLobby { get; set; }
        public IEnumerable<UserInLobby> UserInLobbies { get; set; }
        public async Task<IActionResult> OnGet(string LobbyUID ="")
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/LoginToGame" , new { returnUrl =$"/Lobby/{LobbyUID}"} );
            }
            CurrentUser = await _userManager.GetUserAsync(this.User) as ApplicationUser;
            thisLobby = await lobbyService.GetLobbyWithUID(LobbyUID);
            if (thisLobby == null)
                return RedirectToPage("/Index");
            if(thisLobby.CurrentRound == 0)
            {
                thisLobby.CurrentRound = 1;
                await lobbyService.Update(thisLobby);
                thisLobby = await lobbyService.GetLobbyWithUID(LobbyUID);
            }
            UserInLobbies = await lobbyService.GetUserInLobbiesFromLobby(thisLobby.Id);

            return Page();
        }
        public async Task<ApplicationUser> GetUserWithID(string id)
        {
            var user = await _context.AppUser.FindAsync(id);
            return user;
        }
    }
}
