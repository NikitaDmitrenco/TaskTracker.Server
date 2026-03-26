namespace TaskTracker.Server.Models.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsExecuted { get; set; }
        public bool IsDeleted { get; set; }

    }
}
