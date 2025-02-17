using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    // Add your custom properties
    public string FullName { get; set; }
    public string Role { get; set; }
    public string PasswordHash { get; set; }
}
