namespace ToDo_API.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; } // FK to User who owns this item

        // Navigation property
        public User? User { get; set; }

    }
}
