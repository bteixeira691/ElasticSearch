using Microsoft.EntityFrameworkCore;
using System;
using StudentCore.Models;
using System.Collections.Generic;
using System.Text;

namespace DataBaseSchool.Model
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        { }

        public DbSet<Course> CourseSQL { get; set; }
        public DbSet<Student> StudentSQL { get; set; }
        public DbSet<Enrollment> EnrollmentSQL { get; set; }
        public DbSet<User> UserSQL { get; set; }
    }
}
