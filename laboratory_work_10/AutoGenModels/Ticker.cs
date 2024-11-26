using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace laboratory_work_10;

public partial class Ticker
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "text")]
    public string? TickerName { get; set; }

    [InverseProperty("Ticker")]
    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();

    [InverseProperty("Ticker")]
    public virtual ICollection<TodaysCondition> TodaysConditions { get; set; } = new List<TodaysCondition>();
}
