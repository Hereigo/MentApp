using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Data.EF.Models;

public class User : IdentityUser
{
    [StringLength(100)]
    public string FirstName { get; set; }

    [StringLength(100)]
    public string LastName { get; set; }

    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; }

    public ICollection<ATask> Tasks { get; set; }
}