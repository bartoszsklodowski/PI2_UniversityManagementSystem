using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Enrollment
{
    public int Enrollmentid { get; set; }

    public int Studentid { get; set; }

    public int Courseid { get; set; }

    public string Semester { get; set; } = null!;

    public string Labgrade { get; set; } = null!;

    public string Examgrade { get; set; } = null!;

    public string Finalgrade { get; set; } = null!;

    public string Feestatus { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
