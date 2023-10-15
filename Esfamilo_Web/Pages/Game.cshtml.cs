using Esfamilo_Core.Interfaces;
using Esfamilo_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Esfamilo_Web.Pages
{
    public class GameModel : PageModel
    {
        private ILobbyService lobbyService;
        private ICategoryInLobbyService CategoryInLobbyService;
        private ICategoryService categoryService;
        private UserManager<IdentityUser> _userManager;
        public GameModel(ILobbyService lobbyService, ICategoryInLobbyService categoryInLobbyService, ICategoryService categoryService, UserManager<IdentityUser> userManager)
        {
            this.lobbyService = lobbyService;
            CategoryInLobbyService = categoryInLobbyService;
            this.categoryService = categoryService;
            _userManager = userManager;
        }



        public Lobby CurrentLobby { get; set; }
        public List<Category> CategoryInLobbies { get; set; }

        public async Task<IActionResult> OnGet(string LobbyUID , string TargetLetter)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/LoginToGame", new { returnUrl = $"/Lobby/{LobbyUID}" });
            }
            if (string.IsNullOrEmpty(LobbyUID))
                return RedirectToPage("/Index");

            CurrentLobby = await lobbyService.GetLobbyWithUID(LobbyUID);
            var categoryidlobbyenu = await CategoryInLobbyService.GetCategoryInLobbies(CurrentLobby.Id);
            CategoryInLobbies = categoryidlobbyenu.ToList();
            return Page();
        }
        public IActionResult OnPostCheckWord(string words)
        {
            return new JsonResult("akdfas");
        }
    }
}
