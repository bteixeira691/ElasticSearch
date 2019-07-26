using ApplicationSchool.Model;
using StudentCore.Models;

using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace ApplicationSchool.Operations
{
    public class Mapper
    {
        public static UserAPI userToUserAPI(User user)
        {
            return new UserAPI
            {
                Id = user.Id,
                Password=user.Password,
                Username=user.Username
            };
        }
    }
}
