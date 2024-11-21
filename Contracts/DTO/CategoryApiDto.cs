using System.ComponentModel.DataAnnotations;

namespace Contracts.DTO
{
    public class CategoryApiDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
