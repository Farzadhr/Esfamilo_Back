using Esfamilo_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Esfamilo_Web.Pages
{
    public class IndexModel : PageModel
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public IndexModel(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IFormFile ProfileImgUpdateProfile { get; set; }
        public ApplicationUser CurrentUser { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/LoginToGame");
            }
            CurrentUser = await _userManager.GetUserAsync(this.User) as ApplicationUser;
            return Page();
        }
        public async Task<IActionResult> OnPostUpdateProfileForm(string FullName,string Email)
        {
            string filename = "";
            if (ProfileImgUpdateProfile != null)
            {
                filename = Guid.NewGuid().ToString()+ ProfileImgUpdateProfile.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Profileimg", filename);
                using (var stream = System.IO.File.Create(path))
                {
                    ProfileImgUpdateProfile.CopyTo(stream);
                }
            }
            ApplicationUser user = (ApplicationUser)await _userManager.GetUserAsync(User);
            if (!string.IsNullOrEmpty(FullName))
                user.FullName = FullName;

            if (!string.IsNullOrEmpty(Email))
                user.Email = Email;
            if (!string.IsNullOrEmpty(filename))
                user.ImgProfileUrl = filename;
            await _userManager.UpdateAsync(user);
            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPostSignOutUserForm()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}