using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Payment
{
    public int Paymentid { get; set; }

    public int Studentid { get; set; }

    public int Amount { get; set; }

    public DateOnly Paymentdate { get; set; }

    public string Paymentmethod { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Student Student { get; set; } = null!;
}
