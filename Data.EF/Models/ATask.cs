namespace Data.EF.Models
{
    public class ATask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public Category? Category { get; set; }

        public User User { get; set; }

        public int? CategoryId { get; set; }

        public string UserId { get; set; }
    }
}
