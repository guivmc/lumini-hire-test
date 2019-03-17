using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuminiHire.Models;

namespace LuminiHire.Authentication
{
    public interface IUserService
    {
        User Authenticate( string username, string password );
        UserContext GetUserContext();
    }

}
