using Microsoft.AspNetCore.Identity;

namespace AspNetIdentity.Models
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
