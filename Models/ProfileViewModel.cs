using System;
using System.Collections.Generic;
using University.Areas.Identity.Data;
using University.Models;

public class ProfileViewModel
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Major { get; set; } 
    public string Specialization { get; set; }
    public DateOnly EnrollmentYear { get; set; }
    public decimal Financialbalance { get; set; }
    public List<CourseViewModel> Courses { get; set; }
    public List<Enrollment> Enrollments { get; set; }
    public List<FinancialAid> FinancialAids { get; set; }
    public List<Payment> Payments { get; set; }
}

public class CourseViewModel
{
    public string CourseName { get; set; }
    public string Credits { get; set; }
    public int Tuitionfee { get; set; }
    public string Ects { get; set; }
    public string Roomnumber { get; set; }
    public DateTime Classtime { get; set; }
    public string Facultyname { get; set; }
    public string Departmentname { get; set; }
}

