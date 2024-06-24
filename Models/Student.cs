using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Student
{
    public int Studentid { get; set; }

    public string? Userid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Major { get; set; } = null!;

    public string? Specialization { get; set; }

    public DateOnly Enrollmentyear { get; set; }

    public decimal Financialbalance { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<FinancialAid> FinancialAids { get; set; } = new List<FinancialAid>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual AspNetUser? User { get; set; }
}
