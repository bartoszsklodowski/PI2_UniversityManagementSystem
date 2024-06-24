using System;
using System.Collections.Generic;

namespace University.Models;

public partial class FinancialAid
{
    public int Aidid { get; set; }

    public int Studentid { get; set; }

    public int Amount { get; set; }

    public string Aidtype { get; set; } = null!;

    public DateOnly Awardyear { get; set; }

    public virtual Student Student { get; set; } = null!;
}
