using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratory_work_10;

public partial class Price
{
    [Key]
    public int Id { get; set; }

    public int TickerId { get; set; }

    public double PriceOnDate { get; set; }

    public DateOnly Date { get; set; }

    [ForeignKey("TickerId")]
    [InverseProperty("Prices")]
    public virtual Ticker Ticker { get; set; } = null!;
}
