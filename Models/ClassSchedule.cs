using System;
using System.Collections.Generic;

namespace University.Models;

public partial class ClassSchedule
{
    public int Scheduleid { get; set; }

    public int Courseid { get; set; }

    public int Lecturerid { get; set; }

    public string Roomnumber { get; set; } = null!;

    public DateTime Classtime { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Lecturer Lecturer { get; set; } = null!;
}
