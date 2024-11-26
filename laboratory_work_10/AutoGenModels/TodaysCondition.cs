using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace laboratory_work_10;

public partial class TodaysCondition
{
    [Key]
    public int Id { get; set; }

    public int? TickerId { get; set; }

    [Column(TypeName = "text")]
    public string? State { get; set; }

    [ForeignKey("TickerId")]
    [InverseProperty("TodaysConditions")]
    public virtual Ticker? Ticker { get; set; }
}
