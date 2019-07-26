using Nest;
using StudentCore.Models;
using StudentCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Operations
{
    public class RepositoryElastic<T> : IStudentCore<T> where T : class
    {
        private readonly IElasticClient _cliente;

        public RepositoryElastic(IElasticClient elasticClient)
        {

            _cliente = elasticClient;

        }

        public async Task Add(T entity)
        {

            var type = typeof(T);
            var index = type.Name.ToLowerInvariant();

            var result = await _cliente.IndexAsync(entity , i => i.Index(index));

        }

        public async Task<T> Get(Guid id)
        {
            var type = typeof(T);
            var index = type.Name.ToLowerInvariant();

            var resp = await _cliente.SearchAsync<T>(s => s.Index(index)
             
          
            .Query(qry => qry

                    .QueryString(qs => qs

                        .Query(id + ""))));


            return  resp.Documents.GetEnumerator().Current;
          
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Remove(Guid id )
        {
            var type = typeof(T);
            var index = type.Name.ToLowerInvariant();

           var x2= _cliente.DeleteByQuery<T>(i=> i.Index(index)
    .Query(rq => rq
      .QueryString(qs => qs

                        .Query(id + "")))
    
);

          
        }

        public async Task Update(Guid id, T entity)
        {

            await Remove(id);
            await Add(entity);
        }

    }
}
