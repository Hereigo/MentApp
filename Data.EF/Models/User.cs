using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Data.EF.Models;

public class User : IdentityUser
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; }

    [StringLength(100)]
    public string LastName { get; set; }

    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; }

    public IEnumerable<Task> Tasks { get; set; }
}