using Microsoft.AspNetCore.Identity;

namespace UserLoginRegister.Models;

public class appUser : IdentityUser
{
    public string FullName { get; set; }

    public bool IsActive { get; set; }
}
