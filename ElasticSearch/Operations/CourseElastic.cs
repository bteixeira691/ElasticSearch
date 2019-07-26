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
    public class CourseElastic : RepositoryElastic<Course>, ICourseRepositoryElastic
    {
        private readonly string IndexCourse = "course";
        private readonly IElasticClient _elasticClient;

        public CourseElastic(IElasticClient elasticClient) : base(elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<List<Course>> autoCompleterCourse(string input)
        {
            List<Course> courses = new List<Course>();
            var response = await _elasticClient.SearchAsync<Course>(s => s
                 .Index(IndexCourse)
                 .Type(IndexCourse)
                 .Suggest(su => su
                     .Completion("suggest", cs => cs
                         .Field(f => f.Suggest)
                         .Prefix(input)
                         .Fuzzy(f => f
                             .Fuzziness(Fuzziness.Auto)
                         )
                         .Size(10)
                     )
                 )
             );

            var suggestions =
               from suggest in response.Suggest["suggest"]
               from option in suggest.Options
               let course = new Course
               {
                   CourseID = option.Source.CourseID,
                   Title = option.Source.Title,
               }
               select course;

            var regex = new Regex(Regex.Escape(input.ToLower()));
            foreach (var item in suggestions)
            {
                var lower = item.Title.ToLower();
                var title = regex.Replace(lower, match => "<>" + match.Value + "<>", 1);
                item.Title = title;
                courses.Add(item);
            }
            return courses;
        }

        public async Task Add(Course course)
        {
            Course newCourse = new Course
            {
                CourseID = course.CourseID,
                Credits = course.Credits,
                Title=course.Title.ToLower(),
                Enrollments = course.Enrollments,
                Suggest = new CompletionField
                {
                    Input = new[]
                    {
                        course.Title.ToLower()
                    },
                    Weight = 100
                }
            };
            var rep = await _elasticClient.IndexAsync(newCourse, i=>i.Index(IndexCourse));
        }
    }
}