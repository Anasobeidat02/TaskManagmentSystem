using Domains.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Entities;


[Table("TaskItems")]
public class TaskItem : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(100)]
    [MinLength(0)]
    public string? Description { get; set; }

    public DateTimeOffset? DueDate { get; set; }

     
    public int UserId { get; set; }
    public User User { get; set; }

    public int TaskStatusId { get; set; }
    public TaskStatus TaskStatus { get; set; }
     

    public int TaskPriortyId { get; set; }
    public TaskPriorty TaskPriorty { get; set; } 
}
