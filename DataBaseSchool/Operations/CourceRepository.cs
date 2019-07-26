using DataBaseSchool.Model;
using StudentCore.Interfaces;
using StudentCore.Models;
using System.Threading.Tasks;

namespace DataBaseSchool.Operations
{
    public class CourceRepository : StudentCore<Course>, ICourceRepositorySql
    {
        public CourceRepository(SchoolContext context) : base(context)
        {
        }

        
    }
}
