using RestFullExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullExample.Contracts
{
    public interface IUserService
    {
        User Login(string userName, string password);

    }
}
