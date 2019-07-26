using ApplicationSchool.Interfaces;
using ApplicationSchool.Model;
using AutoMapper;
using StudentCore.Interfaces;
using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationSchool.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IMapper _mapper;
        public SearchService(ISearchRepository searchRepository, IMapper mapper)
        {
            _searchRepository = searchRepository;
            _mapper = mapper;
        }

        public async Task<List<AutoCompleterService>> autoCompleterAll(string name)
        {
            List<AutoCompleterService> autoCompleters = new List<AutoCompleterService>();
            var list = await _searchRepository.AutoCompleterAll(name);
            foreach (var item in list)
            {
                autoCompleters.Add(_mapper.Map<AutoCompleterService>(item));
            }
            return autoCompleters;
        }

        public IEnumerable<SearchResponseView> Search(string input, int page, int pageSize)
        {

           return  _searchRepository.Search(input, page, pageSize).Select(x => _mapper.Map<SearchResponseView>(x));
        }
    }
}
