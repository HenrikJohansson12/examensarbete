using Microsoft.AspNetCore.Identity;

namespace Database.Models;

public class User: IdentityUser
{
    public  string? DisplayName { get; set; }
    public  string? ZipCode { get; set; }

}