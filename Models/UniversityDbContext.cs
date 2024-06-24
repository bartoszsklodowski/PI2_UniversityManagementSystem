using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Models;

public partial class UniversityDbContext : DbContext
{
    public UniversityDbContext()
    {
    }

    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<ClassSchedule> ClassSchedules { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FinancialAid> FinancialAids { get; set; }

    public virtual DbSet<Lecturer> Lecturers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Student> Students { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=UniversityDB;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<ClassSchedule>(entity =>
        {
            entity.HasKey(e => e.Scheduleid).HasName("PK__ClassSch__A533E9EC979B064B");

            entity.HasIndex(e => e.Classtime, "IX_ClassSchedules_ClassTime");

            entity.HasIndex(e => e.Courseid, "IX_ClassSchedules_CourseID");

            entity.Property(e => e.Scheduleid)
                .ValueGeneratedNever()
                .HasColumnName("scheduleid");
            entity.Property(e => e.Classtime).HasColumnName("classtime");
            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.Lecturerid).HasColumnName("lecturerid");
            entity.Property(e => e.Roomnumber)
                .HasMaxLength(10)
                .HasColumnName("roomnumber");

            entity.HasOne(d => d.Course).WithMany(p => p.ClassSchedules)
                .HasForeignKey(d => d.Courseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassSchedules_Courses");

            entity.HasOne(d => d.Lecturer).WithMany(p => p.ClassSchedules)
                .HasForeignKey(d => d.Lecturerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassSchedules_Lecturers");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Courseid).HasName("PK__Courses__2AAB4BC9295A873B");

            entity.HasIndex(e => e.Coursename, "IX_Courses_CourseName");

            entity.Property(e => e.Courseid)
                .ValueGeneratedNever()
                .HasColumnName("courseid");
            entity.Property(e => e.Coursename)
                .HasMaxLength(100)
                .HasColumnName("coursename");
            entity.Property(e => e.Credits)
                .HasMaxLength(10)
                .HasColumnName("credits");
            entity.Property(e => e.Ects)
                .HasMaxLength(10)
                .HasColumnName("ects");
            entity.Property(e => e.Facultyid).HasColumnName("facultyid");
            entity.Property(e => e.Lecturerid).HasColumnName("lecturerid");
            entity.Property(e => e.Tuitionfee).HasColumnName("tuitionfee");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Facultyid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_Faculty");

            entity.HasOne(d => d.Lecturer).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Lecturerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_Lecturer");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Departmentid).HasName("PK__Departme__F9B93045A6725AFE");

            entity.Property(e => e.Departmentid)
                .ValueGeneratedNever()
                .HasColumnName("departmentid");
            entity.Property(e => e.Departmentname)
                .HasMaxLength(100)
                .HasColumnName("departmentname");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("_description");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.Enrollmentid).HasName("PK__Enrollme__ACFE20AE6F54205B");

            entity.HasIndex(e => e.Courseid, "IX_Enrollments_CourseID");

            entity.HasIndex(e => e.Semester, "IX_Enrollments_Semester");

            entity.HasIndex(e => e.Studentid, "IX_Enrollments_StudentID");

            entity.Property(e => e.Enrollmentid)
                .ValueGeneratedNever()
                .HasColumnName("enrollmentid");
            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.Examgrade)
                .HasMaxLength(5)
                .HasColumnName("examgrade");
            entity.Property(e => e.Feestatus)
                .HasMaxLength(20)
                .HasColumnName("feestatus");
            entity.Property(e => e.Finalgrade)
                .HasMaxLength(5)
                .HasColumnName("finalgrade");
            entity.Property(e => e.Labgrade)
                .HasMaxLength(5)
                .HasColumnName("labgrade");
            entity.Property(e => e.Semester)
                .HasMaxLength(15)
                .HasColumnName("semester");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Courseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_Courses");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_Students");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Facultyid).HasName("PK__Facultie__DBBE9399F663A3AC");

            entity.HasIndex(e => e.Departmentid, "IX_Faculties_DepartmentID");

            entity.Property(e => e.Facultyid)
                .ValueGeneratedNever()
                .HasColumnName("facultyid");
            entity.Property(e => e.Departmentid).HasColumnName("departmentid");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("_description");
            entity.Property(e => e.Facultyname)
                .HasMaxLength(50)
                .HasColumnName("facultyname");

            entity.HasOne(d => d.Department).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.Departmentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Faculties_Department");
        });

        modelBuilder.Entity<FinancialAid>(entity =>
        {
            entity.HasKey(e => e.Aidid).HasName("PK__Financia__06C83C382E80BA02");

            entity.HasIndex(e => e.Awardyear, "IX_FinancialAids_AwardYear");

            entity.HasIndex(e => e.Studentid, "IX_FinancialAids_StudentID");

            entity.Property(e => e.Aidid)
                .ValueGeneratedNever()
                .HasColumnName("aidid");
            entity.Property(e => e.Aidtype)
                .HasMaxLength(50)
                .HasColumnName("aidtype");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Awardyear).HasColumnName("awardyear");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Student).WithMany(p => p.FinancialAids)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinancialAids_Students");
        });

        modelBuilder.Entity<Lecturer>(entity =>
        {
            entity.HasKey(e => e.Lecturerid).HasName("PK__Lecturer__79CDD0C76AE9E6FC");

            entity.HasIndex(e => e.Lastname, "IX_Lecturers_Lastname");

            entity.Property(e => e.Lecturerid)
                .ValueGeneratedNever()
                .HasColumnName("lecturerid");
            entity.Property(e => e.Departmentid).HasColumnName("departmentid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("Firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("Lastname");
            entity.Property(e => e.Userid)
                .HasMaxLength(450)
                .HasColumnName("userid");

            entity.HasOne(d => d.Department).WithMany(p => p.Lecturers)
                .HasForeignKey(d => d.Departmentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lecturers_Department");

            entity.HasOne(d => d.User).WithMany(p => p.Lecturers)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_Lecturers_AspNetUsers");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("PK__Payments__AF26EBEECE6964CD");

            entity.HasIndex(e => e.Paymentdate, "IX_Payments_PaymentDate");

            entity.HasIndex(e => e.Studentid, "IX_Payments_StudentID");

            entity.Property(e => e.Paymentid)
                .ValueGeneratedNever()
                .HasColumnName("paymentid");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("_description");
            entity.Property(e => e.Paymentdate).HasColumnName("paymentdate");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(50)
                .HasColumnName("paymentmethod");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Student).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Students");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Studentid).HasName("PK__Students__4D16D2643556564F");

            entity.HasIndex(e => e.Lastname, "IX_Students_Lastname");

            entity.HasIndex(e => e.Major, "IX_Students_Major");

            entity.Property(e => e.Studentid)
                .ValueGeneratedNever()
                .HasColumnName("studentid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Enrollmentyear).HasColumnName("enrollmentyear");
            entity.Property(e => e.Financialbalance)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("financialbalance");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("Firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("Lastname");
            entity.Property(e => e.Major)
                .HasMaxLength(100)
                .HasColumnName("major");
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .HasColumnName("specialization");
            entity.Property(e => e.Userid)
                .HasMaxLength(450)
                .HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_Students_AspNetUsers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
