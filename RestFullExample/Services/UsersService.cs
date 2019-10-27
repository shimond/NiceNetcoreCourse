using RestFullExample.Contracts;
using RestFullExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullExample.Services
{
    public class UsersService : IUserService
    {
        public User Login(string userName, string password)
        {
            if(userName == "shimond@any-techs.co.il" && password == "Aa123456!")
            {
                return new User()
                {
                    ID = 1,
                    Email = "Shimond@any-techs.co.il",
                    LastConnectionTime = DateTime.Now,
                    Password = password,
                    Department = "C1",
                    Role = "Admin"
                };
            }
            return null;
        }
    }
}
