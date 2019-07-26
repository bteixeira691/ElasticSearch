using ApplicationSchool.Model;
using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationSchool.Interfaces
{
    public interface ISearchService
    {
        Task<List<AutoCompleterService>> autoCompleterAll(string name);
        IEnumerable<SearchResponseView> Search(string input, int page, int pageSize);

    }
}
