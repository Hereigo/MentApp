using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Data.EF.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public ICollection<ATask> Tasks { get; set; }
}