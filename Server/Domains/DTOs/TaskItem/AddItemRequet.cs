using Domains.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains.DTOs.TaskItem
{
    public class AddItemRequet
    { 
        public string Title { get; set; } 

        public string? Description { get; set; } 

        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;
         
        public TaskPriortyEnum Priority { get; set; } = TaskPriortyEnum.Low;

        public DateTimeOffset? DueDate { get; set; }
    }
}
