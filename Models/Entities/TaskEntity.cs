namespace TaskTracker.Server.Models.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public bool IsExecuted { get; set; }

        public int UserId { get; set; }
        public UserEntity? User { get; set; }
    }
}
