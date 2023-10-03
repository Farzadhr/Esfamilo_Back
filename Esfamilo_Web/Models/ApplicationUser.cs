using Microsoft.AspNetCore.Identity;

namespace Esfamilo_Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public float? Score { get; set; }
        public string? FullName { get; set; }
        public string? ImgProfileUrl { get; set; }
        public int Win { get; set; }
        public override string Email { get => base.Email; set => base.Email = value; }
    }
}
