using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.DTOs.TaskItem
{
    public class GetAllRespone
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
    }
}
