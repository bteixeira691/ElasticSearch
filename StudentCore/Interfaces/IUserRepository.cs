using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentCore.Interfaces
{
   public  interface IUserRepository : IStudentCore<User>
    {
        Task<User> Authenticate(string username, string password);
    }
}
