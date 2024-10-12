using System.ComponentModel.DataAnnotations;

namespace Data.EF.Models
{
    public class ATask
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsCompleted { get; set; } = false;

        public Category Category { get; set; }

        public User User { get; set; }
    }
}
