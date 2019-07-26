using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentCore.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<AutoCompleter>> AutoCompleterAll(string name);
        IEnumerable<SearchResponse> Search(string input, int page, int pageSize);
    }
}
