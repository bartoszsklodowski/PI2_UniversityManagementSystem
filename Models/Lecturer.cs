using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Lecturer
{
    public int Lecturerid { get; set; }

    public string? Userid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Departmentid { get; set; }

    public virtual ICollection<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Department Department { get; set; } = null!;

    public virtual AspNetUser? User { get; set; }
}
