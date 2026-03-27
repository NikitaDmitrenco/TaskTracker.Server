using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Server.Models.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        public ICollection<TaskEntity>? Tasks { get; set; }
    }
}
