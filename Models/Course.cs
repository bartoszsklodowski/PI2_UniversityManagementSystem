using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Course
{
    public int Courseid { get; set; }

    public int Facultyid { get; set; }

    public int Lecturerid { get; set; }

    public string Coursename { get; set; } = null!;

    public string Credits { get; set; } = null!;

    public int Tuitionfee { get; set; }

    public string Ects { get; set; } = null!;

    public virtual ICollection<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual Lecturer Lecturer { get; set; } = null!;
}
