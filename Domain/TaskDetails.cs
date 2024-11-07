namespace Domain
{
    public class TaskDetails
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public int? CategoryId { get; set; }

        public string UserId { get; set; }
    }
}
