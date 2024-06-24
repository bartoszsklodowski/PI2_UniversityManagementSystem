using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using University.Areas.Identity.Data;
using University.Models;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UniversityDbContext _context;

    public ProfileController(UserManager<ApplicationUser> userManager, UniversityDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var student = await _context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                    .ThenInclude(c => c.ClassSchedules)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                    .ThenInclude(c => c.Faculty)
                        .ThenInclude(f => f.Department)
            .Include(s => s.FinancialAids)
            .Include(s => s.Payments)
            .FirstOrDefaultAsync(s => s.Userid == user.Id);

        if (student == null)
        {
            return NotFound($"Unable to load student with User ID '{user.Id}'.");
        }

        var model = new ProfileViewModel
        {
            UserId = user.Id,
            Username = user.UserName,
            Firstname = student.Firstname,
            Lastname = student.Lastname,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Major = student.Major,
            Specialization = student.Specialization,
            EnrollmentYear = student.Enrollmentyear,
            Financialbalance = student.Financialbalance,
            Enrollments = student.Enrollments.ToList(),
            FinancialAids = student.FinancialAids.ToList(),
            Payments = student.Payments.ToList(),
            Courses = student.Enrollments.Select(e => new CourseViewModel
            {
                CourseName = e.Course.Coursename,
                Credits = e.Course.Credits,
                Tuitionfee = e.Course.Tuitionfee,
                Ects = e.Course.Ects,
                Roomnumber = e.Course.ClassSchedules.FirstOrDefault().Roomnumber,
                Classtime = e.Course.ClassSchedules.FirstOrDefault().Classtime,
                Facultyname = e.Course.Faculty.Facultyname,
                Departmentname = e.Course.Faculty.Department.Departmentname
            }).ToList()
        };

        return View(model);
    }
}
