using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Faculty
{
    public int Facultyid { get; set; }

    public int Departmentid { get; set; }

    public string Facultyname { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Department Department { get; set; } = null!;
}
