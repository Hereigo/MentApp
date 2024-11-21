using System.ComponentModel.DataAnnotations;

namespace Contracts.DTO
{
    public class UserApiDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
