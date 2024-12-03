using System.ComponentModel.DataAnnotations;

namespace AspNetIdentity.ViewModels.Auths
{
    public class LoginVM
    {
        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, MaxLength(32)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } 
    }
}
