using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApiSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoCompleteController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public AutoCompleteController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> AutoCompleter(string name)
        {
            var result = await _searchService.autoCompleterAll(name);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }


    }


}
