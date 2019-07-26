using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class SearchElastic : ISearchRepository
    {
        private readonly IElasticClient _elasticClient;

        public SearchElastic(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;

        }

        public async Task<List<AutoCompleter>> AutoCompleterAll(string name)
        {
                List<AutoCompleter> autoCompleters = new List<AutoCompleter>();

                var response = await _elasticClient.SearchAsync<dynamic>(s => s
                     .AllIndices()
                     .AllTypes()
                     .Suggest(su => su
                         .Completion("suggest", cs => cs
                         .Field("suggest")    
                         .Prefix(name)
                             .Fuzzy(f => f
                                 .Fuzziness(Fuzziness.Auto)
                             )
                             .Size(10)
                         )
                     )
                 );

            var findType = response.Suggest["suggest"];
            var type = findType.Select(s => s.Options);

            var regex = new Regex(Regex.Escape(name.ToLower()));
            foreach (var item in type)
            {
                foreach (var objet in item)
                {
                    var te = objet.Text.ToLower();

                    var tex = regex.Replace(te, match => "<>" + match.Value + "<>", 1);
                    autoCompleters.Add(new AutoCompleter{
                        text= tex,
                        type = objet.Type.Name,
                    });
                }
               
            }
            return autoCompleters;
        }

        public IEnumerable<SearchResponse> Search(string input, int page, int pageSize)
        {
            var resp = _elasticClient.Search<JObject>(s => s
              .AllIndices()
              .AllTypes()
              .From((page - 1) * pageSize)
              .Size(pageSize)
             
               .Query(qry => qry

                       .QueryString(qs => qs

                           .Query(input))));



             return resp.Hits.Select(x => Map(x.Index , x.Source));
          
        }

     

        private SearchResponse Map(string tipo , JObject obj)
        {

            switch (tipo)
            {
                case "student" :
                    Student student = obj.ToObject<Student>();
                    return new SearchResponse{ Id = student.StudentID, Type = tipo, Description = student.FirstName + " " + student.LastName };
                 
                case "course":
                    Course course = obj.ToObject<Course>();
                    return new SearchResponse { Id = course.CourseID, Type = tipo, Description = course.Title};
                   


            }
            return null;
           
        }

       

    }
}

