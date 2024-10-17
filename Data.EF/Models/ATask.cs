namespace Data.EF.Models
{
    public class ATask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsCompleted { get; set; } = false;

        public Category Category { get; set; }

        public User User { get; set; }
    }
}
