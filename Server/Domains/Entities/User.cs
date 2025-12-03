using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Entities;

[Table("Users")]
public class User: BaseEntity
{
    [MaxLength(50)]
    [Required]
    public string FullName { get; set; }

    [MaxLength(100)]
    [Required]
    public string EmailAddress { get; set; }

    [MaxLength(100)]
    [Required]
    public string PasswordHash { get; set; }

    public ICollection<TaskItem>? TaskItem { get; set; } = new List<TaskItem>();
}
