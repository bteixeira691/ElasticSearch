using Nest;
using StudentCore.Interfaces;
using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Operations
{
    public class EnrollmentElastic : RepositoryElastic<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentElastic(IElasticClient elasticClient) : base(elasticClient)
        {

        }
    }
}
