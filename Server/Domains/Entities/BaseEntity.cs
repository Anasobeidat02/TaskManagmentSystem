using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }  

    public DateTimeOffset? UpdatedAt { get; set; }

}
