using Nest;
using StudentCore.Interfaces;
using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElasticSearch.Operations
{
    public class StudentElastic : RepositoryElastic<Student>, IStudentRepositoryElastic
    {
        private readonly string IndexStudent = "student";
        private readonly IElasticClient _elasticClient;
        public StudentElastic(IElasticClient elasticClient) : base(elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<List<Student>> autoCompleterStudent(string input)
        {

            List<Student> Student = new List<Student>();

            var response = await _elasticClient.SearchAsync<Student>(s => s
                 .Index(IndexStudent)
                 .Type(IndexStudent)
                 .Suggest(su => su
                     .Completion("suggest", cs => cs
                         .Field(f => f.Suggest)
                         .Prefix(input)
                         .Fuzzy(f => f
                             .Fuzziness(Fuzziness.Auto)
                         )
                         .Size(10))
                 )
             );

            var suggestions =
               from suggest in response.Suggest["suggest"]
               from option in suggest.Options
               let student = new Student
               {
                   StudentID = option.Source.StudentID,
                   FirstName = option.Source.FirstName,
                   EnrollmentDate = option.Source.EnrollmentDate,
                   Grade = option.Source.Grade,
                   LastName = option.Source.LastName,

               }
               select student;

            var regex = new Regex(Regex.Escape(input.ToLower()));
            foreach (var item in suggestions)
            {
                var lastLower = item.LastName.ToLower();
                var firstLower = item.FirstName.ToLower();

                var first = regex.Replace(firstLower, match => "<>" + match.Value + "<>", 1);
                var last = regex.Replace(lastLower, match => "<>" + match.Value + "<>", 1);

                item.FirstName = first;
                item.LastName = last;
                Student.Add(item);
            }
            return Student;
        }

        public async Task Add(Student student)
        {
            Student newStudent = new Student
            {
                EnrollmentDate = student.EnrollmentDate,
                LastName = student.LastName,
                FirstName = student.FirstName,
                Grade = student.Grade,
                StudentID = student.StudentID,
                Suggest = new CompletionField
                {
                    Input = new[]
                    {
                        student.LastName.ToLower(),
                        student.FirstName.ToLower()
                    },
                    Weight = 200
                }
            };
            var rep = await _elasticClient.IndexAsync(newStudent, i => i.Index(IndexStudent));
        }
    }
}
