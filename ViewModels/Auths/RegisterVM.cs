using System.ComponentModel.DataAnnotations;

namespace AspNetIdentity.ViewModels.Auths;

public class RegisterVM
{
    [Required, MaxLength(64)]
    public string Fullname { get; set; }

    [Required, MaxLength(128), DataType(DataType.EmailAddress)]
    public string EmailAdress { get; set; }

    [Required, MaxLength(128)]
    public string Username { get; set; }

    [Required, MaxLength(32), DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required, MaxLength(32), DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
