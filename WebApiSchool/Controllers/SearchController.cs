using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationSchool.Interfaces;
using ApplicationSchool.Model;
using Microsoft.AspNetCore.Mvc;




using Newtonsoft.Json;
using StudentCore.Interfaces;

namespace WebApiSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        
        private readonly ISearchService _search;


        public SearchController(ISearchService search)
        {
            _search = search;

        }

        // POST: api/Index
        [HttpPost("{input}")]
        public IEnumerable<SearchResponseView> Search(string input, int page, int pageSize, string sort, string field)
        {
            var result = _search.Search(input, page, pageSize);
            return result;
        }
    



    }


}
