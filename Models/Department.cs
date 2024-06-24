using System;
using System.Collections.Generic;

namespace University.Models;

public partial class Department
{
    public int Departmentid { get; set; }

    public string Departmentname { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();

    public virtual ICollection<Lecturer> Lecturers { get; set; } = new List<Lecturer>();
}
