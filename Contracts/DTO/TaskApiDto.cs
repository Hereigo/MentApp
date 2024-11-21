using System.ComponentModel.DataAnnotations;

namespace Contracts.DTO
{
    public class TaskApiDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Description { get; set; }
    }
}
