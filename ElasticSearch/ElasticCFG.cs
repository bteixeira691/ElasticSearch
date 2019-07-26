using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Nest.JsonNetSerializer;
using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch
{
    public static class ElasticCFG
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var indexCourse = configuration["elasticsearch:indexCourse"];
            
            var pool = new SingleNodeConnectionPool(new Uri(url));
            var Nodesettings = new ConnectionSettings(pool, sourceSerializer: JsonNetSerializer.Default);

            var client = new ElasticClient(Nodesettings);
            var verIndexCourse = client.IndexExists(indexCourse);

            if (!verIndexCourse.Exists)
                client.CreateIndex(indexCourse, s => s
                   .Settings(s1 => s1
                    .NumberOfShards(5)
                    .NumberOfReplicas(2)
                    .Analysis(ab => ab
                    .CharFilters(cf => cf
                                 .Mapping("programming_language", mca => mca
                                     .Mappings(new[]
                                     {
                        "c# => csharp",
                        "r#=> rsharp",
                        "f# => fsharp",
                        "objective-c => objective c",
                        "c++ => cplusplus",
                        "js => javascript"
                                     })
                                 ))
                     .TokenFilters(t => t
                      .EdgeNGram("edge", ed => ed
                      .MaxGram(7)
                      .MinGram(3)
                      ))
                     .Analyzers(a1 => a1
                        .Custom("greek", t => t.Tokenizer("standard")
                        .CharFilters("html_strip", "programming_language")
                        .Filters("edge", "standard", "lowercase", "stop", "asciifolding")))
                        ))
                   .Mappings(mc => mc
                         .Map<Course>(mmc => mmc
                         .AutoMap().
                          Properties(p => p.Text(t => t.Name(f => f.Title).Analyzer("greek")
                         )))));


            var indexStudent = configuration["elasticsearch:indexStudent"];
            var verIndexStudent = client.IndexExists(indexStudent);

            if (!verIndexStudent.Exists)
                client.CreateIndex(indexStudent, s => s
                   .Settings(s1 => s1
                    .NumberOfShards(5)
                    .NumberOfReplicas(2)
                    .Analysis(ab => ab
                    .CharFilters(cf => cf
                                 .Mapping("programming_language", mca => mca
                                     .Mappings(new[]
                                     {
                        "c# => csharp",
                        "r#=> rsharp",
                        "f# => fsharp",
                        "objective-c => objective c",
                        "c++ => cplusplus",
                        "js => javascript"
                                     })
                                 ))
                     .TokenFilters(t => t
                      .EdgeNGram("edge", ed => ed
                      .MaxGram(7)
                      .MinGram(3)
                      ))
                     .Analyzers(a1 => a1
                        .Custom("greek", t => t.Tokenizer("standard")
                        .CharFilters("html_strip", "programming_language")
                        .Filters("edge", "standard", "lowercase", "stop", "asciifolding")))
                        ))
                   .Mappings(mc => mc
                         .Map<Student>(mmc => mmc
                         .AutoMap()
                         .Properties(p => p.Text(t => t.Name(f => f.LastName).Analyzer("greek")))
                          .Properties(p => p.Text(t => t.Name(f => f.FirstName).Analyzer("greek"))))
                         ));

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
