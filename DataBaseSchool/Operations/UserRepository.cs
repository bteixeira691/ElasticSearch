using DataBaseSchool.Model;
using StudentCore.Interfaces;
using StudentCore.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseSchool.Operations
{
    public class UserRepository : StudentCore<User>, IUserRepository
    {
        private readonly SchoolContext _context;
        public UserRepository(SchoolContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = from qwe in _context.UserSQL
                    where qwe.Username.Equals(username) && qwe.Password.Equals(password)
                    select qwe;

            if (user == null)
                return null;
            return user.FirstOrDefault();
        }
    }
}
