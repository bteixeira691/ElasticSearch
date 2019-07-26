using DataBaseSchool.Model;
using Microsoft.EntityFrameworkCore.Internal;
using StudentCore.Models;
using System;
using System.Collections.Generic;

using System.Text;

namespace DataBaseSchool
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            // context.Database.EnsureCreated();

            // Look for any students.
            if (context.StudentSQL.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01"), StudentID= new Guid("b53bccee-43ff-4901-8885-7037cee0596f")},
            new Student{FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01"), StudentID =new Guid("41e90dca-e540-48fa-b4d7-056bbd05d97a")},
            new Student{FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01"), StudentID = new Guid("d5234f6b-6448-4477-8c87-4cf93a0251ca")},
            new Student{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01"), StudentID = new Guid ("9dadea32-071a-4eae-9fde-668f24cc29e3")},
            new Student{FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01"), StudentID= new Guid("062a7700-f165-4418-ae1f-ecf2f061a7e1")},
            new Student{FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01"), StudentID= new Guid("5c6e5715-7c1b-4629-a421-fcc3ac0567fb")},
            new Student{FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01"), StudentID= new Guid("090f4511-a07c-42ab-8c54-225d0ab7942c")},
            new Student{FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01"), StudentID= new Guid("182609f6-6f65-404c-830c-bf803c774f93")}
            };
            foreach (Student s in students)
            {
                context.StudentSQL.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseID= new Guid("16c80884-8564-4f2b-9a56-e37a857cd8f4"),Title="Chemistry",Credits=3},
            new Course{CourseID= new Guid("912574b1-2a7c-48b6-95c7-bc192c9ff74b"),Title="Microeconomics",Credits=3},
            new Course{CourseID= new Guid("412d459e-5184-45df-a761-6e6ad7c4f6a0"),Title="Macroeconomics",Credits=3},
            new Course{CourseID= new Guid("7d8268cb-2040-4d24-8db6-db3acae802da"),Title="Calculus",Credits=4},
            new Course{CourseID= new Guid("1e05b2af-c122-4d8d-8414-c346f25d5e4d"),Title="Trigonometry",Credits=4},
            new Course{CourseID= new Guid("fe5642a6-43a2-47bb-b7e0-f2798dd49b31"),Title="Composition",Credits=3},
            new Course{CourseID= new Guid("43ea5305-eb70-483e-9a35-a0f655cfc8b1"),Title="Literature",Credits=4}
            };
            foreach (Course c in courses)
            {
                context.CourseSQL.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=new Guid("b53bccee-43ff-4901-8885-7037cee0596f"),CourseID=new Guid("16c80884-8564-4f2b-9a56-e37a857cd8f4"),Grade=Grade.A},
            new Enrollment{StudentID=new Guid("b53bccee-43ff-4901-8885-7037cee0596f"),CourseID=new Guid("912574b1-2a7c-48b6-95c7-bc192c9ff74b"),Grade=Grade.C},
            new Enrollment{StudentID=new Guid("b53bccee-43ff-4901-8885-7037cee0596f"),CourseID=new Guid("412d459e-5184-45df-a761-6e6ad7c4f6a0"),Grade=Grade.B},
            new Enrollment{StudentID=new Guid("41e90dca-e540-48fa-b4d7-056bbd05d97a"),CourseID=new Guid("7d8268cb-2040-4d24-8db6-db3acae802da"),Grade=Grade.B},
            new Enrollment{StudentID=new Guid("41e90dca-e540-48fa-b4d7-056bbd05d97a"),CourseID=new Guid("1e05b2af-c122-4d8d-8414-c346f25d5e4d"),Grade=Grade.F},
            new Enrollment{StudentID=new Guid("182609f6-6f65-404c-830c-bf803c774f93"),CourseID=new Guid("fe5642a6-43a2-47bb-b7e0-f2798dd49b31"),Grade=Grade.F},
            new Enrollment{StudentID=new Guid("d5234f6b-6448-4477-8c87-4cf93a0251ca"),CourseID=new Guid("16c80884-8564-4f2b-9a56-e37a857cd8f4"),Grade=Grade.B},
            new Enrollment{StudentID=new Guid("9dadea32-071a-4eae-9fde-668f24cc29e3"),CourseID=new Guid("16c80884-8564-4f2b-9a56-e37a857cd8f4"),Grade=Grade.B},
            new Enrollment{StudentID=new Guid("9dadea32-071a-4eae-9fde-668f24cc29e3"),CourseID=new Guid("43ea5305-eb70-483e-9a35-a0f655cfc8b1"),Grade=Grade.F},
            new Enrollment{StudentID=new Guid("062a7700-f165-4418-ae1f-ecf2f061a7e1"),CourseID=new Guid("412d459e-5184-45df-a761-6e6ad7c4f6a0"),Grade=Grade.C},
            new Enrollment{StudentID=new Guid("5c6e5715-7c1b-4629-a421-fcc3ac0567fb"),CourseID=new Guid("7d8268cb-2040-4d24-8db6-db3acae802da"),Grade=Grade.B},
            new Enrollment{StudentID=new Guid("090f4511-a07c-42ab-8c54-225d0ab7942c"),CourseID=new Guid("1e05b2af-c122-4d8d-8414-c346f25d5e4d"),Grade=Grade.A},
            };
            foreach (Enrollment e in enrollments)
            {
                context.EnrollmentSQL.Add(e);
            }
            context.SaveChanges();

            var user = new User[]
            {
                new User{Id = new Guid(), Password="teste", Username="teste"}
            };
            foreach (var item in user)
            {
                context.UserSQL.Add(item);
            }
            context.SaveChanges();
        }
    }
}
